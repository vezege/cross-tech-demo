using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Domain.Entities;
using System.Collections.Generic;

namespace CrossTech.Persistence.Gateways
{
    /// <summary>
    /// Класс-хранилище действий, для которых требуется доступ
    /// </summary>
    public class PermissionGateway : BasicReadOnlyGateway<Permission>, IPermissionGateway
    {
        protected override IEnumerable<Permission> InitializeCollection()
        {
            return new List<Permission>
            {
                new Permission { Id = 1, Code = "see_users_list", Name = "Просмотр список пользователей" },
                new Permission { Id = 2, Code = "see_employees_list", Name = "Просмотр списка сотрудников" },
                new Permission { Id = 3, Code = "create_employee", Name = "Создание записи сотрудника" },
                new Permission { Id = 4, Code = "update_employee", Name = "Обновление записи сотрудника" },
                new Permission { Id = 5, Code = "delete_employee", Name = "Удаление записи сотрудника" }
            };
        }
    }
}
