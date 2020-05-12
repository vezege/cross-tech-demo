using CrossTech.Application.DTO;
using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Application.Interfaces.UseCases.PositionsUseCases;
using CrossTech.Application.Models;
using CrossTech.Domain.Entities;
using System.Collections.Generic;

namespace CrossTech.Application.UseCases.PositionsUseCases.RequestPositions
{
    public class RequestPositionsUseCase : BasicUseCase, IRequestPositionsUseCase
    {
        private readonly IPositionGateway _positionGateway;

        public RequestPositionsUseCase(IPositionGateway positionGateway)
        {
            _positionGateway = positionGateway;
        }

        /// <summary>
        /// Возвращаем список должностей
        /// </summary>
        /// <returns></returns>
        public UseCaseResponse<RequestPositionsUseCaseResult> Handle()
        {
            IEnumerable<PositionModel> positions = _mapper.Map<IEnumerable<Position>, IEnumerable<PositionModel>>(_positionGateway.GetAll());

            return new UseCaseResponse<RequestPositionsUseCaseResult>(true, new RequestPositionsUseCaseResult { Positions = positions });
        }
    }
}
