using CrossTech.Application.DTO;
using CrossTech.Application.UseCases;
using CrossTech.Application.UseCases.EmployeesUseCases.RemoveEmployee;

namespace CrossTech.Application.Interfaces.UseCases.EmployeesUseCases
{
    public interface IRemoveEmployeeUseCase : IParameterizedUseCase<RemoveEmployeeUseCaseRequest, UseCaseResponse<EmptyUseCaseResult>, EmptyUseCaseResult>
    {
    }
}
