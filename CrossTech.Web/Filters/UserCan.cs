using CrossTech.Application.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CrossTech.Web.Filters
{
    /// <summary>
    /// Атрибут, производящий проверку доступа пользователя к определенному действию
    /// </summary>
    public class UserCan : TypeFilterAttribute
    {
        public UserCan(string action) : base(typeof(UserCanImplementation))
        {
            Arguments = new object[] { action };
        }

        private class UserCanImplementation : IActionFilter
        {
            private readonly string _action;
            private readonly IAccessCheckManager _accessManager;

            public UserCanImplementation(IAccessCheckManager accessManager, string action)
            {
                _accessManager = accessManager;
                _action = action;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                int userId = int.Parse(context.HttpContext.User.Identity.Name);
                bool userCan = _accessManager.UserCan(userId, _action);
                if (!userCan)
                {
                    context.Result = new ViewResult { ViewName = "~/Views/Errors/403.cshtml", StatusCode = 403 };
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}
