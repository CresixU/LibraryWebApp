using NLog.Web;
using LibraryAPI.Application;
using LibraryAPI.Application.Middleware;
using LibraryAPI.Infrastructure;
using LibraryAPI.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LibraryAPI.Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

//NLog config
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Add services to the container.

//JWT Configuration
builder.ConfigureAuthentication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddDomain();
builder.AddInfrastructure();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policyBuilder =>
        policyBuilder.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins(builder.Configuration["AllowedOrigins"])
    );
});



var app = builder.Build();

app.UseCors("FrontEndClient");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
