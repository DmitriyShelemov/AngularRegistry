using FluentValidation;

namespace Web.Models.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Agreed)
                .Must(x => x)
                .WithMessage("Please agree to work for food.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("Email must be a valid one.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(@"^(?=.*[A-Z])(?=.*\d)[^\s]{2,}$")
                .WithMessage("Password must contain min 1 digit and min 1 uppercase letter.");

            RuleFor(x => x.CountryId)
                .GreaterThan(0)
                .WithMessage("Country is a required field.");

            RuleFor(x => x.ProvinceId)
                .GreaterThan(0)
                .WithMessage("Province is a required field.")
                .When(x => x != null);
        }
    }
}
