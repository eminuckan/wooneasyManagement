using Carter;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;
using WooneasyManagement.API;
using WooneasyManagement.API.Middleware;
using WooneasyManagement.Application;
using WooneasyManagement.Infrastructure;
using WooneasyManagement.Infrastructure.Services.Storage;
using WooneasyManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddStorage<LocalStorage>();

builder.Services.AddAuthorization();
builder.Services.AddCarter();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Woon Easy Management API", Version = "v1" });
    c.UseInlineDefinitionsForEnums();
    c.SchemaFilter<EnumSchemaFilter>();
});

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("Postgres"), "ApplicationLogs",
        needAutoCreateTable: true)
    .CreateLogger();

builder.Host.UseSerilog(logger);
builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddAntiforgery();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAntiforgery();
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapCarter();

app.Run();