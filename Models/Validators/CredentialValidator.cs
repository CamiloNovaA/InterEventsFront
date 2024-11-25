using FluentValidation;
using Models.DTO.User;

namespace Models.Validators
{
    public class CredentialValidator : AbstractValidator<UserCredentialDTO>
    {
        public CredentialValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(100);
        }
    }
}
