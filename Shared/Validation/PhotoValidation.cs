using FluentValidation;
using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Shared.Validation;

public class PhotoValidation: AbstractValidator<Photo>
{
    public PhotoValidation()
    {
        RuleFor(p => p.Title).NotEmpty().WithMessage("Title cannot be empty!");
        RuleFor(p => p.Title).MaximumLength(100).WithMessage("Title cannot be longer than 100 characters!");
        RuleFor(p => p.Description).NotEmpty().WithMessage("Description cannot be empty!");
        RuleFor(p => p.Description).MaximumLength(2000).WithMessage("Description cannot be longer than 2000 characters!");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<Photo>.CreateWithOptions((Photo)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
