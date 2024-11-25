using FluentValidation;
using Models.DTO;

namespace Models.Validators
{
    public class EventValidator : AbstractValidator<EventDTO>
    {
        public EventValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MinimumLength(10);
            RuleFor(x => x.DateEvent).NotEmpty().GreaterThan(DateTime.Now);
            RuleFor(x => x.IdCity).NotEmpty();
            RuleFor(x => x.Latitude).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Longitude).NotEmpty().MaximumLength(20);
            RuleFor(x => x.Capacity).NotEmpty().GreaterThan(10);
            RuleFor(x => x.IdOwner).NotEmpty();
    }
    }
}
