using CrossTech.Domain.Entities;

namespace CrossTech.Application.Interfaces.Gateways
{
    public interface IEmployeeGateway : IBasicGateway<Employee>
    {
        void FillEmployee(Employee employee);

        Employee FindByUserId(int userId);
    }
}
