using CrossTech.Domain.Entities;

namespace CrossTech.Application.Interfaces.Gateways
{
    public interface IPositionGateway : IBasicGateway<Position>
    {
        Position Find(int id);
    }
}
