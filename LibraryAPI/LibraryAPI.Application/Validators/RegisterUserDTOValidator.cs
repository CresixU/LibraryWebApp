using FluentValidation;
using LibraryAPI.Application.Models.Account;

namespace LibraryAPI.Application.Validators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator(LibraryContext dbContext)
        {
            RuleFor(x => x.Firstname)
                .NotEmpty();

            RuleFor(x => x.Lastname)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                        context.AddFailure("Email", "That email is taken.");
                });
        }
    }
}
