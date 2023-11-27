using Asp.Versioning;
using GhostBank.Application.Interface;
using GhostBank.Application.Services;
using GhostBank.Domain.Interfaces;
using GhostBank.Domain.Services;
using GhostBank.Infrastructure.Data.Contexts;
using GhostBank.Infrastructure.Repository.Interfaces;
using GhostBank.Infrastructure.Repository.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace GhostBank.Infrastructure.CrossCutting;

public static class ConfigureServiceCollection
{
	public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
	{
		Console.WriteLine("Starting configure dependency injection...");

		services.AddDbContext<GhostBankContext>();

		services.AddScoped(typeof(IApplicationUserService), typeof(ApplicationUserService));
		services.AddScoped(typeof(IDomainUserService), typeof(DomainUserService));
		services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

		services.AddScoped(typeof(IUserClaimRepository), typeof(UserClaimRepository));

		services.AddScoped(typeof(IApplicationAuthenticationService), typeof(ApplicationAuthenticationService));
		services.AddScoped(typeof(IDomainAuthenticationService), typeof(DomainAuthenticationService));

		services.AddScoped(typeof(IRepositoryWrapper), typeof(RepositoryWrapper));
		services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

		Console.WriteLine("Configuration of dependency injection finished");

		return services;
	}

	public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
	{
		Console.WriteLine("Starting configuration services...");

		services
			.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				Console.WriteLine("Starting configuration JWT Bearer Authorization...");

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

				Console.WriteLine("Starting configuration JWT Bearer Authorization...");
			});

		services.AddSwaggerGen(x => 
		{
			x.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
			{
				Description =
					"JWT Authorization Header - Utilizado com Bearer Authentication.\r\n\r\n" +
					"Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
					"Exemplo (informar sem as aspas): 'Bearer token123'",

				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.ApiKey,
				Scheme = JwtBearerDefaults.AuthenticationScheme,
				BearerFormat = "JWT",
			});

			x.AddSecurityRequirement(new OpenApiSecurityRequirement
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

		services.AddApiVersioning(options =>
		{
			options.DefaultApiVersion = new ApiVersion(1, 0);
			options.AssumeDefaultVersionWhenUnspecified = true;
			options.ReportApiVersions = true;
		});

		Console.WriteLine("Configuration services finished");

		return services;
	}
}
