using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CrossTech.Persistence.Gateways
{
    /// <summary>
    /// Класс-хранилище записей сотрудников
    /// </summary>
    public class EmployeeGateway : BasicGateway<Employee>, IEmployeeGateway
    {
        private readonly IUserProfileGateway _profileGateway;
        private readonly IPositionGateway _positionGateway;

        public EmployeeGateway(IUserProfileGateway profileGateway, IPositionGateway positionGateway)
        {
            _profileGateway = profileGateway;
            _positionGateway = positionGateway;
            foreach (Employee employee in _entities)
            {
                FillEmployee(employee);
            }
        }

        protected override IList<Employee> InitializeCollection()
        {
            return new List<Employee>
            {
                new Employee { Id = 1, PositionId = 1, UserId = 1 },
                new Employee { Id = 2, PositionId = 2, UserId = 2 },
                new Employee { Id = 3, PositionId = 3, UserId = 3 }
            };
        }

        public Employee FindByUserId(int userId)
        {
            return Find(e => e.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// Заполняем связанные сущности, связанные с записью сотрудника
        /// </summary>
        /// <param name="employee"></param>
        public void FillEmployee(Employee employee)
        {
            employee.Position = _positionGateway.Find(employee.PositionId);
            employee.Profile = _profileGateway.FindByUserId(employee.UserId);
        }

        /// <summary>
        /// Заполняем ID новой сущности и помещаем её в список
        /// </summary>
        /// <param name="newEmployee"></param>
        public override void Add(Employee newEmployee)
        {
            Employee lastEmployee = _entities.Last();
            newEmployee.Id = lastEmployee.Id + 1;
            FillEmployee(newEmployee);
            base.Add(newEmployee);
        }
    }
}
