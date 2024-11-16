using FluentValidation;
using Promo.TestTask.Api.Requests;
using Promo.TestTask.Domain.Account.Services;

namespace Promo.TestTask.Api.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public const string INVALID_EMAIL_MESSAGE = "Please enter a valid email";
    public const string EMAIL_REQUIRED_MESSAGE = "Email should not be empty";
    public const string EMAIL_EXISTS_MESSAGE = "User with same email already exist";
    public const string INVALID_PASSWORD_MESSAGE = "Password must contain at least one digit and one letter.";
    public const string PASSWORD_REQUIRED_MESSAGE = "Password should not be empty";
    public const string MUST_AGREE_MESSAGE = "Agree must be checked";

    public CreateUserValidator(IUserService userService)
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(EMAIL_REQUIRED_MESSAGE)
            .EmailAddress()
            .WithMessage(INVALID_EMAIL_MESSAGE)
            .MustAsync(async (email, ct) =>
            {
                var existingUser = await userService.Get(email);
                return existingUser == null;
            }).WithMessage(EMAIL_EXISTS_MESSAGE);


        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(PASSWORD_REQUIRED_MESSAGE)
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)")
            .WithMessage(INVALID_PASSWORD_MESSAGE);

        RuleFor(x => x.IsAgreed)
            .Must(x => x)
            .WithMessage(MUST_AGREE_MESSAGE);
    }
}
