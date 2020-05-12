using CrossTech.Application.DTO;
using CrossTech.Application.UseCases.PositionsUseCases.RequestPositions;

namespace CrossTech.Application.Interfaces.UseCases.PositionsUseCases
{
    public interface IRequestPositionsUseCase : IParameterlessUseCase<UseCaseResponse<RequestPositionsUseCaseResult>, RequestPositionsUseCaseResult>
    {
    }
}
