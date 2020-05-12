using CrossTech.Application.Interfaces.UseCases;

namespace CrossTech.Application.UseCases.EmployeesUseCases.RemoveEmployee
{
    public class RemoveEmployeeUseCaseRequest : IUseCaseRequest
    {
        public int Id { get; set; }
    }
}
