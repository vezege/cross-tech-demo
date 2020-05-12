using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Domain.Entities;
using System.Collections.Generic;

namespace CrossTech.Persistence.Gateways
{
    public class RolePermissionGateway : BasicReadOnlyGateway<RolePermission>, IRolePermissionGateway
    {
        protected override IEnumerable<RolePermission> InitializeCollection()
        {
            return new List<RolePermission>
            {
                new RolePermission { PermissionId = 1, RoleId = 1 },
                new RolePermission { PermissionId = 2, RoleId = 1 },
                new RolePermission { PermissionId = 3, RoleId = 1 },
                new RolePermission { PermissionId = 4, RoleId = 1 },
                new RolePermission { PermissionId = 5, RoleId = 1 },
                new RolePermission { PermissionId = 2, RoleId = 2 }
            };
        }
    }
}
