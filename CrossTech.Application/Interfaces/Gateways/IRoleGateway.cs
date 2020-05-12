using CrossTech.Domain.Entities;

namespace CrossTech.Application.Interfaces.Gateways
{
    public interface IRoleGateway : IBasicGateway<Role>
    {
        Role FindByCode(string code);
        Role Find(int id);
    }
}
