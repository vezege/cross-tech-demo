using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CrossTech.Persistence.Gateways
{
    /// <summary>
    /// Класс-хранилище должностей сотрудников
    /// </summary>
    public class PositionGateway : BasicGateway<Position>, IPositionGateway
    {
        protected override IList<Position> InitializeCollection()
        {
            return new List<Position>
            {
                new Position { Id = 1, Name = "Младший сотрудник" },
                new Position { Id = 2, Name = "Старший сотрудник" },
                new Position { Id = 3, Name = "Сотрудник отдела кадров" }
            };
        }

        public Position Find(int id)
        {
            return Find(p => p.Id == id).First();
        }
    }
}
