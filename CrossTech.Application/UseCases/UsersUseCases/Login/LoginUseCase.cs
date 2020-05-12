using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Application.Interfaces.UseCases.UsersUseCases;
using CrossTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossTech.Application.UseCases.UsersUseCases.Login
{
    public class LoginUseCase : BasicParameterizedUseCase<LoginUseCaseRequest, LoginUseCaseResult, LoginUseCaseValidator>,
                                ILoginUseCase
    {
        private readonly IUserGateway _userGateway;

        public LoginUseCase(IUserGateway userGateway)
        {
            _userGateway = userGateway;
        }

        /// <summary>
        /// Ищем пользователя, соответствующего введенным данным, и возвращаем его ID
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        protected override LoginUseCaseResult Handler(LoginUseCaseRequest loginData)
        {
            IEnumerable<User> searchResult = _userGateway.Find(user => user.Login == loginData.Login && user.Password == loginData.Password);
            if (searchResult.Count() > 0)
            {
                User user = searchResult.First();
                return new LoginUseCaseResult { Id = user.Id };
            }
            else
            {
                throw new Exception("Пользователь не найден");
            }
        }
    }
}
