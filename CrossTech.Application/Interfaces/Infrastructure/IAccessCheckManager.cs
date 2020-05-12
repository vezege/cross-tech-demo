using System.Collections.Generic;

namespace CrossTech.Application.Interfaces.Infrastructure
{
    public interface IAccessCheckManager
    {
        bool UserCan(int userId, string permissionCode);
        IEnumerable<string> UserCan(int userId, IEnumerable<string> permissionCodes);
    }
}
