using FluentValidation;
using WebApplication1.Models;

namespace WebApplication1.Validators;

public class UserRegisterModelValidator : AbstractValidator<UserRegisterModel>
{
    public UserRegisterModelValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .EmailAddress().WithMessage("Username must be a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one digit.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Age)
            .NotEmpty().WithMessage("Age is required.")
            .GreaterThan(17).WithMessage("Age must be greater than 17.");

        RuleFor(x => x.Weight)
            .NotEmpty().WithMessage("Weight is required.")
            .GreaterThan(0).WithMessage("Weight must be greater than 0.");

        RuleFor(x => x.Height)
            .NotEmpty().WithMessage("Height is required.")
            .GreaterThan(0).WithMessage("Height must be greater than 0.");

        RuleFor(x => x.Goal)
            .NotEmpty().WithMessage("Goal is required.");
    }
}