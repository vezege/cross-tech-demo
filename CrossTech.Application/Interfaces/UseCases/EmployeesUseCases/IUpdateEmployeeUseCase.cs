using CrossTech.Application.DTO;
using CrossTech.Application.UseCases.EmployeesUseCases.UpdateEmployee;

namespace CrossTech.Application.Interfaces.UseCases.EmployeesUseCases
{
    public interface IUpdateEmployeeUseCase : IParameterizedUseCase<UpdateEmployeeUseCaseRequest, UseCaseResponse<UpdateEmployeeUseCaseResult>, UpdateEmployeeUseCaseResult>
    {
    }
}
