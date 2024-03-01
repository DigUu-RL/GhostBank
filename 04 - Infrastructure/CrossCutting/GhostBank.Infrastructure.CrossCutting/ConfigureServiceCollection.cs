using Asp.Versioning;
using GhostBank.Application.Interface.Authentication;
using GhostBank.Application.Interface.Bank;
using GhostBank.Application.Interface.Identity;
using GhostBank.Application.Services.Authentication;
using GhostBank.Application.Services.Bank;
using GhostBank.Application.Services.Identity;
using GhostBank.Domain.Interfaces.Authentication;
using GhostBank.Domain.Interfaces.Bank;
using GhostBank.Domain.Interfaces.Identity;
using GhostBank.Domain.Services.Authentication;
using GhostBank.Domain.Services.Bank;
using GhostBank.Domain.Services.Identity;
using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Data.Contexts.Audit;
using GhostBank.Infrastructure.Middleware;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Interfaces.Audit;
using GhostBank.Infrastructure.Repository.Interfaces.Audit.Identity;
using GhostBank.Infrastructure.Repository.Interfaces.Bank;
using GhostBank.Infrastructure.Repository.Interfaces.Identity;
using GhostBank.Infrastructure.Repository.Repositories;
using GhostBank.Infrastructure.Repository.Repositories.Audit;
using GhostBank.Infrastructure.Repository.Repositories.Audit.Identity;
using GhostBank.Infrastructure.Repository.Repositories.Bank;
using GhostBank.Infrastructure.Repository.Repositories.Identity;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Text;

namespace GhostBank.Infrastructure.CrossCutting;

public static class ConfigureServiceCollection
{
	public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
	{
		services.AddDbContext<GhostBankContext>();
		services.AddDbContext<GhostBankAuditContext>();

		services.AddHttpContextAccessor();

		#region AUDIT

		#region IDENTITY

		services.AddScoped(typeof(IUserAuditRepository), typeof(UserAuditRepository));

		#endregion

		services.AddScoped(typeof(IBaseAuditRepository<>), typeof(BaseAuditRepository<>));

		#endregion

		#region BANK

		services.AddScoped(typeof(IApplicationAccountService), typeof(ApplicationAccountService));
		services.AddScoped(typeof(IDomainAccountService), typeof(DomainAccountService));
		services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));

		#endregion

		#region IDENTITY

		services.AddScoped(typeof(IApplicationUserService), typeof(ApplicationUserService));
		services.AddScoped(typeof(IDomainUserService), typeof(DomainUserService));
		services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

		services.AddScoped(typeof(IUserClaimRepository), typeof(UserClaimRepository));

		#endregion

		#region AUTHENTICATION

		services.AddScoped(typeof(IApplicationAuthenticationService), typeof(ApplicationAuthenticationService));
		services.AddScoped(typeof(IDomainAuthenticationService), typeof(DomainAuthenticationService));

		services.AddScoped(typeof(IApplicationJwtService), typeof(ApplicationJwtService));
		services.AddScoped(typeof(IDomainJwtService), typeof(DomainJwtService));

		#endregion

		services.AddScoped(typeof(IRepositoryWrapper), typeof(RepositoryWrapper));
		services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

		return services;
	}

	public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = true;
				x.SaveToken = true;

				IConfigurationSection section = configuration.GetSection("JWT");

				string audience = section.GetValue<string>("Audience")!;
				string issuer = section.GetValue<string>("Issuer")!;
				byte[] key = Encoding.UTF8.GetBytes(section.GetValue<string>("Key")!);

				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = audience,
					ValidIssuer = issuer,
					IssuerSigningKey = new SymmetricSecurityKey(key)
				};
			});

		services.AddSwaggerGen(options =>
		{
			options.CustomSchemaIds(x => x.FullName);

			options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
			{
				Description =
					"JWT Authorization Header - Utilizado com Bearer Authentication.\r\n\r\n" +
					"Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
					"Exemplo (informar sem as aspas): 'Bearer token123'",

				Name = nameof(Authorization),
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = JwtBearerDefaults.AuthenticationScheme,
				BearerFormat = "JWT",
			});

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					Array.Empty<string>()
				}
			});
		});

		services
			.AddApiVersioning(x =>
			{
				x.DefaultApiVersion = new ApiVersion(1);
				x.AssumeDefaultVersionWhenUnspecified = true;
				x.ReportApiVersions = true;
				x.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("x-version"));
			}).AddApiExplorer(x =>
			{
				x.GroupNameFormat = "'v'VVV";
				x.SubstituteApiVersionInUrl = true;
			});

		services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("GhostBankHangfire")));
		services.AddHangfireServer();

		return services;
	}

	public static async Task<IServiceCollection> EnsureDatabaseAsync(this IServiceCollection services, IConfiguration configuration)
	{
		using (var context = new GhostBankContext(configuration))
		{
			if (!await context.Database.CanConnectAsync())
				await context.Database.EnsureCreatedAsync();
		}

		using (var context = new GhostBankAuditContext(configuration))
		{
			if (!await context.Database.CanConnectAsync())
				await context.Database.EnsureCreatedAsync();

		}

		return services;
	}

	public static IApplicationBuilder AddMiddlewares(this IApplicationBuilder builder)
	{
		builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
		builder.UseMiddleware(typeof(JwtMiddleware));

		return builder;
	}
}
