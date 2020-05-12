using CrossTech.Application.DTO;
using CrossTech.Application.UseCases.EmployeesUseCases.AddEmployee;

namespace CrossTech.Application.Interfaces.UseCases.EmployeesUseCases
{
    public interface IAddEmployeeUseCase : IParameterizedUseCase<AddEmployeeUseCaseRequest, UseCaseResponse<AddEmployeeUseCaseResult>, AddEmployeeUseCaseResult>
    {
    }
}
