using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Application.Interfaces.UseCases.EmployeesUseCases;
using CrossTech.Domain.Entities;
using System;
using System.Linq;

namespace CrossTech.Application.UseCases.EmployeesUseCases.RemoveEmployee
{
    /// <summary>
    /// Обработчик запроса "Удаление сотрудника"
    /// </summary>
    public class RemoveEmployeeUseCase : BasicParameterizedUseCase<RemoveEmployeeUseCaseRequest, EmptyUseCaseResult, RemoveEmployeeUseCaseValidator>,
                                         IRemoveEmployeeUseCase
    {
        private readonly IUserGateway _userGateway;
        private readonly IUserProfileGateway _profileGateway;
        private readonly IEmployeeGateway _employeeGateway;

        public RemoveEmployeeUseCase(IEmployeeGateway employeeGateway, IUserGateway userGateway, IUserProfileGateway profileGateway)
        {
            _employeeGateway = employeeGateway;
            _userGateway = userGateway;
            _profileGateway = profileGateway;
        }

        /// <summary>
        /// Удаляем запись сотрудника и связанные с ней записи пользователя и пользовательской информации
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override EmptyUseCaseResult Handler(RemoveEmployeeUseCaseRequest request)
        {
            Employee employee = _employeeGateway.Find(e => e.Id == request.Id).First();
            if (employee == null)
            {
                throw new Exception($"Сотрудник не может быть удален, т.к. не существует сотрудника с ID {request.Id}");
            }
            int userId = employee.UserId;

            _employeeGateway.Remove(employee);
            _profileGateway.Remove(p => p.UserId == userId);
            _userGateway.Remove(user => user.Id == userId);

            return new EmptyUseCaseResult();
        }
    }
}
