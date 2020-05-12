using FluentValidation.Results;

namespace CrossTech.Application.Interfaces.UseCases
{
    public interface IUseCaseRequestValidator<TRequest>
    {
        ValidationResult Validate(TRequest data);
    }
}
