using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Application.Interfaces.UseCases.EmployeesUseCases;
using CrossTech.Application.Models;
using CrossTech.Domain.Entities;
using CrossTech.Domain.Enumerations;
using CrossTech.Domain.ValueObjects;
using System;
using System.Linq;

namespace CrossTech.Application.UseCases.EmployeesUseCases.UpdateEmployee
{
    /// <summary>
    /// Обработчик запроса "Обновление данных о сотруднике"
    /// </summary>
    public class UpdateEmployeeUseCase : BasicParameterizedUseCase<UpdateEmployeeUseCaseRequest, UpdateEmployeeUseCaseResult, UpdateEmployeeUseCaseValidator>,
                                         IUpdateEmployeeUseCase
    {
        private readonly IUserProfileGateway _profileGateway;
        private readonly IEmployeeGateway _employeeGateway;

        public UpdateEmployeeUseCase(IUserProfileGateway profileGateway, IEmployeeGateway employeeGateway)
        {
            _profileGateway = profileGateway;
            _employeeGateway = employeeGateway;
        }

        /// <summary>
        /// Проверяем, существует ли сотрудник, чью запись хотят обновить, и заменяем в ней данные.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Обновленная запись сотрудника</returns>
        protected override UpdateEmployeeUseCaseResult Handler(UpdateEmployeeUseCaseRequest request)
        {
            Employee employee = _employeeGateway.Find(e => e.Id == request.Id).First();
            if (employee == null)
            {
                throw new Exception("Сотрудник не найден");
            }

            employee.PositionId = request.PositionId;

            UserProfile profile = _profileGateway.Find(p => p.UserId == employee.UserId).First();
            profile.FirstName = request.FirstName;
            profile.LastName = request.LastName;
            profile.MiddleName = request.MiddleName;
            profile.Birthdate = request.Birthdate;
            profile.Phone = Phone.For(request.Phone);
            profile.Gender = (ProfileGender)request.Gender;

            _employeeGateway.FillEmployee(employee);

            return new UpdateEmployeeUseCaseResult { Employee = _mapper.Map<EmployeeModel>(employee) };
        }
    }
}
