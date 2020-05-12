using CrossTech.Application.Interfaces.UseCases.UsersUseCases;
using CrossTech.Web.DTO;
using CrossTech.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossTech.Web.Controllers
{
    [Authorize]
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly IRequestUsersListUseCase _listUC;

        public UsersController(IRequestUsersListUseCase listUC)
        {
            _listUC = listUC;
        }

        /// <summary>
        /// Просмотр всех пользователей системы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [UserCan("see_users_list")]
        public IActionResult Index()
        {
            var result = _listUC.Handle();
            if (result.Success)
            {
                return Ok(new Response(true, result.Result.Users));
            }
            else
            {
                return StatusCode(500, new Response(result.Reason));
            }
        }
    }
}