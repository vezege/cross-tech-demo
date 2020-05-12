using CrossTech.Application.Interfaces.UseCases;

namespace CrossTech.Application.UseCases.UsersUseCases.Login
{
    public class LoginUseCaseRequest : IUseCaseRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
