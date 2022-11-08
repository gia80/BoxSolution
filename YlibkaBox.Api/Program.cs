using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using YlibkaBox.Api.Dal.Repositories;
using YlibkaBox.Api.Domain.Configurations;
using YlibkaBox.Api.Domain.Contracts;
using YlibkaBox.Api.Infrastructure.Authentications;
using YlibkaBox.Api.Infrastructure.Extensions;
using YlibkaBox.Api.Infrastructure.Factories;
using YlibkaBox.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var assemblyConfigurationAttribute = typeof(Program).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>();
var configurationName = assemblyConfigurationAttribute?.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json", true)
    .AddJsonFile($"appsettings.{configurationName}.json", true)
    .Build();

builder.Services.AddOptions()
    .Configure<DatabaseConfiguration>(configuration.GetSection(nameof(DatabaseConfiguration)))
    .AddSingleton(configuration);


var dateFormat = configuration.GetSection("DateFormat").Value;
var databaseConfiguration = configuration.GetSection(nameof(DatabaseConfiguration)).Get<DatabaseConfiguration>();


builder.Services.AddCors();
builder.Services.AddCorsPolicy();
builder.Services.AddControllerConfiguration(dateFormat);
builder.Services.AddDb(databaseConfiguration);


builder.Services.AddTransient<IObjectResultFactory, ObjectResultFactory>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBoxRepository, BoxRepository>();
builder.Services.AddTransient<IBoxService, BoxService>();


builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


if (!builder.Environment.IsProduction())
    builder.Services.AddSwagger();

var app = builder.Build();


if (app.Environment.IsDevelopment())
    app.UseHttpsRedirection();
else if (app.Environment.IsStaging())
    app.UseForwardedHeaders();

if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseStatusCodePages();
    app.UseStaticFiles();
    app.UseSwaggerConfiguration();
}
else
{
    app.UseHsts();
}

app.UseCors("CorsPolicy");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


await app.RunAsync();