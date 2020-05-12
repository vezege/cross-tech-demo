using CrossTech.Application.Interfaces.UseCases;
using CrossTech.Application.Models;
using System.Collections.Generic;

namespace CrossTech.Application.UseCases.UsersUseCases.RequestUsersList
{
    public class RequestUsersListUseCaseResult : IUseCaseResult
    {
        public IEnumerable<UserModel> Users;
    }
}
