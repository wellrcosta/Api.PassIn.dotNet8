using FluentValidation;
using PassIn.Communication.Requests;

namespace PassIn.Infrastructure.Validators;

public class RequestEventJsonValidator : AbstractValidator<RequestEventJson>
{
    public RequestEventJsonValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Details).NotEmpty().WithMessage("Details are required");
        RuleFor(x => x.MaximumAttendees).GreaterThan(0).WithMessage("Maximum attendees must be greater than zero");
        RuleFor(x => x.MaximumAttendees).LessThan(400).WithMessage("Maximum attendees must be lesser than zero");
    }
}