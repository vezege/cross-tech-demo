using CrossTech.Application.DTO;
using CrossTech.Application.UseCases.UsersUseCases.Login;

namespace CrossTech.Application.Interfaces.UseCases.UsersUseCases
{
    public interface ILoginUseCase : IParameterizedUseCase<LoginUseCaseRequest, UseCaseResponse<LoginUseCaseResult>, LoginUseCaseResult>
    {
    }
}
