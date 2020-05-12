using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CrossTech.Persistence.Gateways
{
    /// <summary>
    /// Класс-хранилище ролей пользователей приложения
    /// </summary>
    public class RoleGateway : BasicGateway<Role>, IRoleGateway
    {
        protected override IList<Role> InitializeCollection()
        {
            return new List<Role>
            {
                new Role { Id = 1, Name = "Администратор", Code = "admin" },
                new Role { Id = 2, Name = "Линейный пользователь", Code = "user" }
            };
        }

        public Role FindByCode(string code)
        {
            return Find(x => x.Code == code).FirstOrDefault();
        }

        public Role Find(int id)
        {
            return Find(p => p.Id == id).FirstOrDefault();
        }
    }
}
