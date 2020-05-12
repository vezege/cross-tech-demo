using CrossTech.Application.Interfaces.UseCases;
using CrossTech.Application.Models;
using System.Collections.Generic;

namespace CrossTech.Application.UseCases.PositionsUseCases.RequestPositions
{
    public class RequestPositionsUseCaseResult : IUseCaseResult
    {
        public IEnumerable<PositionModel> Positions { get; set; }
    }
}
