namespace CrossTech.Application.Interfaces.UseCases
{
    public interface IUseCaseResponse<T> where T : IUseCaseResult
    {
        bool Success { get; set; }
        T Result { get; set; }
    }
}
