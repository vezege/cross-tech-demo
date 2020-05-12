using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Domain.Entities;
using System.Collections.Generic;

namespace CrossTech.Persistence.Gateways
{
    public class UserRoleGateway : BasicGateway<UserRole>, IUserRoleGateway
    {
        private readonly IRoleGateway _roleGateway;

        public UserRoleGateway(IRoleGateway roleGateway)
        {
            _roleGateway = roleGateway;
            foreach (UserRole userRole in _entities)
            {
                userRole.Role = roleGateway.Find(userRole.RoleId);
            }
        }
        protected override IList<UserRole> InitializeCollection()
        {
            return new List<UserRole>
            {
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 2, RoleId = 2 },
                new UserRole { UserId = 3, RoleId = 2 }
            };
        }
    }
}
