using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Application.Interfaces.Infrastructure;
using CrossTech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossTech.Application.Infrastructure
{
    /// <summary>
    /// Класс, отвечающий за проверку доступа пользователя к выполнению действия
    /// </summary>
    public class AccessCheckManager : IAccessCheckManager
    {
        private readonly IRolePermissionGateway _rpGateway;
        private readonly IPermissionGateway _permissionGateway;
        private readonly IUserRoleGateway _urGateway;

        public AccessCheckManager(IUserRoleGateway urGateway, IPermissionGateway permissionGateway, IRolePermissionGateway rpGateway)
        {
            _urGateway = urGateway;
            _permissionGateway = permissionGateway;
            _rpGateway = rpGateway;
        }

        /// <summary>
        /// Проверяем, имеет ли пользователь доступ к выполнению действия
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        public bool UserCan(int userId, string permissionCode)
        {
            Permission permission = _permissionGateway.Find(p => p.Code == permissionCode).FirstOrDefault();
            if (permission == null)
            {
                throw new Exception($"Действие {permissionCode} не найдено");
            }

            UserRole userRole = _urGateway.Find(ur => ur.UserId == userId).FirstOrDefault();
            if (userRole == null)
            {
                throw new Exception($"Не найден список доступа пользователя");
            }

            return _rpGateway.Find(c => c.PermissionId == permission.Id && c.RoleId == userRole.RoleId).Count() > 0;
        }

        /// <summary>
        /// Проверяем список действий на то, имеет ли пользователь к ним доступ, и возвращаем список тех, к которым имеет.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="permissionCodes"></param>
        /// <returns></returns>
        public IEnumerable<string> UserCan(int userId, IEnumerable<string> permissionCodes)
        {
            IEnumerable<Permission> permissions = _permissionGateway.Find(p => permissionCodes.Contains(p.Code));

            UserRole userRole = _urGateway.Find(ur => ur.UserId == userId).FirstOrDefault();
            if (userRole == null)
            {
                throw new Exception($"Не найден список доступных действий для пользователя");
            }

            IEnumerable<int> rolePermissions = _rpGateway.Find(p => p.RoleId == userRole.RoleId).Select(rp => rp.PermissionId);

            return from permission in permissions
                   where rolePermissions.Contains(permission.Id)
                   select permission.Code;
        }
    }
}
