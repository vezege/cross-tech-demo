using CrossTech.Application.Interfaces.UseCases;
using CrossTech.Application.Models;
using System.Collections.Generic;

namespace CrossTech.Application.UseCases.EmployeesUseCases.RequestEmployeeList
{
    public class RequestEmployeeListUseCaseResult : IUseCaseResult
    {
        public IEnumerable<EmployeeModel> Employees { get; set; }
        public IEnumerable<string> RequesterPermissions { get; set; }
    }
}
