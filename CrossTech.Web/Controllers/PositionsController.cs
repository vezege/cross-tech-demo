using CrossTech.Application.Interfaces.UseCases.PositionsUseCases;
using CrossTech.Web.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossTech.Web.Controllers
{
    [Authorize]
    public class PositionsController : Controller
    {
        private readonly IRequestPositionsUseCase _requestPositionsUseCase;

        public PositionsController(IRequestPositionsUseCase requestPositionsUseCase)
        {
            _requestPositionsUseCase = requestPositionsUseCase;
        }

        /// <summary>
        /// Просмотр всех возможных должностей
        /// </summary>
        /// <returns></returns>
        [Route("positions")]
        public IActionResult Index()
        {
            var positionsResult = _requestPositionsUseCase.Handle();
            if (positionsResult.Success)
            {
                return new JsonResult(new Response(true, positionsResult.Result.Positions));
            }
            else
            {
                return BadRequest(new Response(false));
            }
        }
    }
}