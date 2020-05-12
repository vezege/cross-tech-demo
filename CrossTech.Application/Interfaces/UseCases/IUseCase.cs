using CrossTech.Application.DTO;

namespace CrossTech.Application.Interfaces.UseCases
{
    public interface IUseCase
    {
    }

    public interface IParameterizedUseCase<TRequest, TResponse, TResult> : IUseCase
        where TRequest : IUseCaseRequest
        where TResponse : IUseCaseResponse<TResult>
        where TResult : IUseCaseResult
    {
        TResponse Handle(TRequest request);
        IUseCaseResponse<ValidationResult> Validate(TRequest request);
    }

    public interface IParameterlessUseCase<TResponse, TResult> : IUseCase
        where TResponse : IUseCaseResponse<TResult>
        where TResult : IUseCaseResult
    {
        TResponse Handle();
    }
}
