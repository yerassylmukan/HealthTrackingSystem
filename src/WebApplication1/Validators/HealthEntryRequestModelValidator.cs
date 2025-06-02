using FluentValidation;
using WebApplication1.Models;

namespace WebApplication1.Validators;

public class HealthEntryRequestModelValidator : AbstractValidator<HealthEntryRequestModel>
{
    public HealthEntryRequestModelValidator()
    {
        RuleFor(x => x.Calories)
            .NotEmpty().WithMessage("Calories is required")
            .GreaterThan(0).WithMessage("Calories must be greater than 0");

        RuleFor(x => x.Steps)
            .NotEmpty().WithMessage("Steps is required")
            .GreaterThan(0).WithMessage("Steps must be greater than 0");

        RuleFor(x => x.SleepHours)
            .NotEmpty().WithMessage("SleepHours is required")
            .GreaterThan(0).WithMessage("SleepHours must be greater than 0");
        
        RuleFor(x => x.Mood)
            .NotEmpty().WithMessage("Mood is required");
    }
}