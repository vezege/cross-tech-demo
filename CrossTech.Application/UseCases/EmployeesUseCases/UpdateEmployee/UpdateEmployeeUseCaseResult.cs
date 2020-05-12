using CrossTech.Application.Interfaces.UseCases;
using CrossTech.Application.Models;

namespace CrossTech.Application.UseCases.EmployeesUseCases.UpdateEmployee
{
    public class UpdateEmployeeUseCaseResult : IUseCaseResult
    {
        public EmployeeModel Employee { get; set; }
    }
}
