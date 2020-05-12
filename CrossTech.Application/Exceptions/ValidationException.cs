using System;
using System.Collections.Generic;

namespace CrossTech.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; private set; }
        public ValidationException(Dictionary<string, List<string>> errors) : base("Переданные данные некорректны")
        {
            Errors = errors;
        }
    }
}
