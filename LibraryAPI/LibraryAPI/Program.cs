using FluentValidation.AspNetCore;
using FluentValidation;
using LibraryAPI;
using LibraryAPI.Entities;
using LibraryAPI.Middleware;
using LibraryAPI.Models.Account;
using LibraryAPI.Services;
using LibraryAPI.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LibraryAPI.Data.Context;
using LibraryAPI.Data.Seeds;
using LibraryAPI.Data;
using LibraryAPI.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

//NLog config
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Add services to the container.

//JWT Configuration
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
builder.Services.AddScoped<IValidator<LoginUserDTO>, LoginUserDTOValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();
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

builder.Services.AddDbContext<LibraryContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnectionString"))
    );
/*builder.Services.AddControllers().AddJsonOptions(
    option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
    );*/

builder.Services.RegisterSeeds();
await builder.Services.ExecuteSeed();

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

await builder.Services.MigrateDatabase();

app.Run();
