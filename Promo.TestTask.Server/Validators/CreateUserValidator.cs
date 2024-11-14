using FluentValidation;
using Promo.TestTask.Api.Requests;

namespace Promo.TestTask.Api.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Please enter a valid email");

        RuleFor(x => x.Password)
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)")
            .WithMessage("Password must contain at least one digit and one letter.");
    }
}
