using CrossTech.Application.DTO;
using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Application.Interfaces.UseCases.UsersUseCases;
using CrossTech.Application.Models;
using CrossTech.Domain.Entities;
using System.Collections.Generic;

namespace CrossTech.Application.UseCases.UsersUseCases.RequestUsersList
{
    public class RequestUsersListUseCase : BasicUseCase, IRequestUsersListUseCase
    {
        private readonly IUserGateway _userGateway;

        public RequestUsersListUseCase(IUserGateway userGateway)
        {
            _userGateway = userGateway;
        }

        /// <summary>
        /// Возвращаем список пользователей системы
        /// </summary>
        /// <returns></returns>
        public UseCaseResponse<RequestUsersListUseCaseResult> Handle()
        {
            IEnumerable<UserModel> users = _mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(_userGateway.GetAll());
            return new UseCaseResponse<RequestUsersListUseCaseResult>(true, new RequestUsersListUseCaseResult { Users = users });
        }
    }
}
