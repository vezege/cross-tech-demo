using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Application.Interfaces.UseCases.EmployeesUseCases;
using CrossTech.Application.Models;
using CrossTech.Domain.Entities;
using CrossTech.Domain.ValueObjects;
using System;
using System.Linq;

namespace CrossTech.Application.UseCases.EmployeesUseCases.AddEmployee
{
    /// <summary>
    /// Обработчик запроса "Создать нового сотрудника"
    /// </summary>
    public class AddEmployeeUseCase : BasicParameterizedUseCase<AddEmployeeUseCaseRequest, AddEmployeeUseCaseResult, AddEmployeeUseCaseValidator>,
                                      IAddEmployeeUseCase
    {
        private readonly IEmployeeGateway _employeeGateway;
        private readonly IUserGateway _userGateway;
        private readonly IUserProfileGateway _profileGateway;

        public AddEmployeeUseCase(IEmployeeGateway employeeGateway, IUserGateway userGateway, IUserProfileGateway profileGateway)
        {
            _employeeGateway = employeeGateway;
            _userGateway = userGateway;
            _profileGateway = profileGateway;
        }

        /// <summary>
        /// Создаем запись пользователя, его профиля и его должности.
        /// Возвращаем информацию о новом сотруднике.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override AddEmployeeUseCaseResult Handler(AddEmployeeUseCaseRequest request)
        {
            // ищем, не существовало ли пользователя с таким именем, и создаем, если не было
            User currentUser = FindUser(request.Login);
            if (currentUser != null)
            {
                throw new Exception("Данное имя пользователя уже занято");
            }
            _userGateway.Add(new User { Login = request.Login, Password = request.Password });
            User newUser = FindUser(request.Login);

            // сохраняем информацию о пользователе
            _profileGateway.Add(new UserProfile
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Phone = Phone.For(request.Phone),
                Gender = (Domain.Enumerations.ProfileGender)request.Gender,
                Birthdate = request.Birthdate,
                UserId = newUser.Id
            });

            // записываем данные о сотруднике
            _employeeGateway.Add(new Employee
            {
                UserId = newUser.Id,
                PositionId = request.PositionId
            });

            Employee newEmployee = _employeeGateway.FindByUserId(newUser.Id);

            return new AddEmployeeUseCaseResult { Employee = _mapper.Map<EmployeeModel>(newEmployee) };
        }

        private User FindUser(string Login)
        {
            return _userGateway.Find(user => user.Login == Login).FirstOrDefault();
        }
    }
}
