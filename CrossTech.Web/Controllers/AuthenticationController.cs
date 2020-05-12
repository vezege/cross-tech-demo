using CrossTech.Application.Interfaces.UseCases.UsersUseCases;
using CrossTech.Application.UseCases.UsersUseCases.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrossTech.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILoginUseCase _loginUseCase;

        public AuthenticationController(ILoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        /// <summary>
        /// Отображение страницы для авторизации
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="loginData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUseCaseRequest loginData)
        {
            var validationResult = _loginUseCase.Validate(loginData);
            if (!validationResult.Success)
            {
                foreach (KeyValuePair<string, List<string>> invalidField in validationResult.Result.Errors)
                {
                    foreach (string error in invalidField.Value)
                    {
                        ModelState.AddModelError(invalidField.Key, error);
                    }
                }
                return View("Login", loginData);
            }

            var loginResult = _loginUseCase.Handle(loginData);

            if (loginResult.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginResult.Result.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "CrossTechCookies");
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync("CrossTechCookies", new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Employees");
            }
            else
            {
                throw loginResult.Reason;
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CrossTechCookies");
            return RedirectToAction("Login", "Authentication");
        }
    }
}