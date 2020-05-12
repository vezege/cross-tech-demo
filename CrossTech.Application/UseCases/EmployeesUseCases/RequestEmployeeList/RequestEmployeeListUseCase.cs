using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Application.Interfaces.Infrastructure;
using CrossTech.Application.Interfaces.UseCases.EmployeesUseCases;
using CrossTech.Application.Models;
using CrossTech.Domain.Entities;
using System.Collections.Generic;

namespace CrossTech.Application.UseCases.EmployeesUseCases.RequestEmployeeList
{
    /// <summary>
    /// Обработчик запроса "Запрос списка сотрудников"
    /// </summary>
    public class RequestEmployeeListUseCase : BasicParameterizedUseCase<BasicUserUseCaseRequest, RequestEmployeeListUseCaseResult, BasicUserUseCaseRequestValidator<BasicUserUseCaseRequest>>,
                                              IRequestEmployeeListUseCase
    {
        private readonly IEmployeeGateway _employeeGateway;
        private readonly IAccessCheckManager _accessManager;
        /// <summary>
        /// Список действий, которые могут потребоваться пользователю для работы со списком
        /// </summary>
        private readonly IEnumerable<string> _extraPermissions = new List<string>
            {
                "see_users_list",
                "create_employee",
                "update_employee",
                "delete_employee"
            };

        public RequestEmployeeListUseCase(IEmployeeGateway employeeGateway, IAccessCheckManager accessManager)
        {
            _employeeGateway = employeeGateway;
            _accessManager = accessManager;
        }

        /// <summary>
        /// Получение списка сотрудников и доступных пользователю действий, которые могут понадобиться для работы с пользователями
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override RequestEmployeeListUseCaseResult Handler(BasicUserUseCaseRequest request)
        {
            IEnumerable<Employee> entities = _employeeGateway.GetAll();

            return new RequestEmployeeListUseCaseResult
            {
                Employees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeModel>>(entities),
                RequesterPermissions = _accessManager.UserCan(request.Requester.Id, _extraPermissions)
            };
        }
    }
}
