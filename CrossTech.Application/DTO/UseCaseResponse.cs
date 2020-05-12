using CrossTech.Application.Interfaces.UseCases;
using System;

namespace CrossTech.Application.DTO
{
    public class UseCaseResponse<T> : IUseCaseResponse<T>
        where T : IUseCaseResult
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public Exception Reason { get; set; }
        private UseCaseResponse()
        {
        }

        public UseCaseResponse(bool success, T result)
        {
            Success = success;
            Result = result;
        }

        public UseCaseResponse(bool success) : this(success, default)
        {
            if (!success && Reason == null)
            {
                throw new Exception("Не указана причина неудачной обработки пользовательского запроса");
            }
        }

        public UseCaseResponse(Exception reason)
        {
            Success = false;
            Reason = reason;
        }
    }
}
