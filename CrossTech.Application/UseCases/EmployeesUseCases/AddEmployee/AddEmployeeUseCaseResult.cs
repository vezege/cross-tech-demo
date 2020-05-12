using CrossTech.Application.Interfaces.UseCases;
using CrossTech.Application.Models;

namespace CrossTech.Application.UseCases.EmployeesUseCases.AddEmployee
{
    public class AddEmployeeUseCaseResult : IUseCaseResult
    {
        public EmployeeModel Employee { get; set; }
    }
}
