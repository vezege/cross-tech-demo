using CrossTech.Application.DTO;
using System;

namespace CrossTech.Web.DTO
{
    /// <summary>
    /// Класс унификации ответа
    /// </summary>
    public class Response
    {
        public bool Success { get; set; }
        public object Payload { get; set; }

        public Response(bool success, object payload)
        {
            Success = success;
            Payload = payload;
        }

        public Response(bool success) : this(success, null)
        {
        }

        public Response(Exception exception) : this(false)
        {
            Payload = exception != null ? exception.Message : "Что-то пошло не так";
        }

        public Response(ValidationResult validationResult) : this(false)
        {
            Payload = validationResult.Errors;
        }
    }
}
