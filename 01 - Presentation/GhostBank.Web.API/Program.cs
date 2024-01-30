using GhostBank.Infrastructure.CrossCutting;
using Hangfire;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();
builder.Services.AddAuthorization();

// configure service collection and dependency injection
builder.Services.AddDependencyInjection();
builder.Services.ConfigureServices(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseHangfireDashboard();
}

app.UseCors(x =>
{
	x.AllowAnyHeader();
	x.AllowAnyMethod();
	x.AllowAnyOrigin();
});

app.AddMiddlewares();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
