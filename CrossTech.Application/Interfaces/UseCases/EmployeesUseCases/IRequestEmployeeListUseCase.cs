using CrossTech.Application.DTO;
using CrossTech.Application.UseCases;
using CrossTech.Application.UseCases.EmployeesUseCases.RequestEmployeeList;

namespace CrossTech.Application.Interfaces.UseCases.EmployeesUseCases
{
    public interface IRequestEmployeeListUseCase : IParameterizedUseCase<BasicUserUseCaseRequest, UseCaseResponse<RequestEmployeeListUseCaseResult>, RequestEmployeeListUseCaseResult>
    {
    }
}
