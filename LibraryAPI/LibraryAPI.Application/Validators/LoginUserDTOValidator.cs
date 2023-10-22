using FluentValidation;
using LibraryAPI.Application.Models.Account;

namespace LibraryAPI.Application.Validators
{
    public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserDTOValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotEmpty();
        }
    }
}
