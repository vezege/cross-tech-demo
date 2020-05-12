using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CrossTech.Persistence.Gateways
{
    /// <summary>
    /// Класс-хранилище пользователей приложения
    /// </summary>
    public class UserGateway : BasicGateway<User>, IUserGateway
    {
        private readonly IUserRoleGateway _urGateway;

        public UserGateway(IUserRoleGateway urGateway)
        {
            _urGateway = urGateway;
            foreach (User user in _entities)
            {
                user.Role = _urGateway.Find(ur => ur.UserId == user.Id).Select(ur => ur.Role).FirstOrDefault();
            }
        }

        protected override IList<User> InitializeCollection()
        {
            return new List<User>
            {
                new User { Id = 1, Login = "admin", Password = "admin" },
                new User { Id = 2, Login = "foo", Password = "fooPassword" },
                new User { Id = 3, Login = "bar", Password = "barPassword" }
            };
        }

        /// <summary>
        /// Заполняем ID новой сущности, задаем ей роль линейного пользователя и помещаем её в список
        /// </summary>
        /// <param name="newUser"></param>
        public override void Add(User newUser)
        {
            User lastUser = _entities.Last();
            newUser.Id = lastUser.Id + 1;
            base.Add(newUser);
            _urGateway.Add(new UserRole { UserId = newUser.Id, RoleId = 2 });
        }
    }
}
