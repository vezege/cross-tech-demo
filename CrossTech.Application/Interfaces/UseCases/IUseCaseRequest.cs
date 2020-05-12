using CrossTech.Application.DTO;

namespace CrossTech.Application.Interfaces.UseCases
{
    public interface IUseCaseRequest
    {
    }

    public interface IUserUseCaseRequest : IUseCaseRequest
    {
        public Requester Requester { get; set; }
    }
}
