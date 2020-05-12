using CrossTech.Application.Interfaces.UseCases;
using System.Collections.Generic;

namespace CrossTech.Application.DTO
{
    public class ValidationResult : IUseCaseResult
    {
        public ValidationResult()
        {
            Errors = new Dictionary<string, List<string>>();
        }
        public Dictionary<string, List<string>> Errors { get; set; }
        public bool IsValid
        {
            get
            {
                return Errors.Count == 0;
            }
        }
    }
}
