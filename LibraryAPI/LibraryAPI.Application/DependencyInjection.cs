using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using FluentValidation;
using LibraryAPI.Application.Services;
using LibraryAPI.Application.Application.Services;
using Microsoft.AspNetCore.Identity;
using LibraryAPI.Application.Models.Account;
using LibraryAPI.Application.Middleware;
using LibraryAPI.Application.Validators;

namespace LibraryAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IBookService, BookService>();
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IRoleService, RoleService>();
            service.AddScoped<IRentService, RentService>();
            service.AddScoped<IAccountService, AccountService>();

            service.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            service.AddScoped<IValidator<RegisterUserDTO>, RegisterUserDTOValidator>();
            service.AddScoped<IValidator<LoginUserDTO>, LoginUserDTOValidator>();
            service.AddFluentValidationAutoValidation();
            service.AddScoped<ErrorHandlingMiddleware>();
            service.AddScoped<RequestTimeMiddleware>();

            return service;
        }
    }
}