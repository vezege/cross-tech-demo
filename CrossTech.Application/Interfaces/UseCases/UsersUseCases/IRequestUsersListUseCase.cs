using CrossTech.Application.DTO;
using CrossTech.Application.UseCases.UsersUseCases.RequestUsersList;

namespace CrossTech.Application.Interfaces.UseCases.UsersUseCases
{
    public interface IRequestUsersListUseCase : IParameterlessUseCase<UseCaseResponse<RequestUsersListUseCaseResult>, RequestUsersListUseCaseResult>
    {
    }
}
