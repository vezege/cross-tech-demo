using CrossTech.Application.DTO;
using CrossTech.Application.Exceptions;
using CrossTech.Application.Interfaces.UseCases;
using System;
using System.Collections.Generic;

namespace CrossTech.Application.UseCases
{
    /// <summary>
    /// Базовый класс UseCase, принимающих на вход какие-либо параметры
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TValidator"></typeparam>
    public abstract class BasicParameterizedUseCase<TRequest, TResult, TValidator> : BasicUseCase,
                                                                                     IParameterizedUseCase<TRequest, UseCaseResponse<TResult>, TResult>
        where TRequest : IUseCaseRequest
        where TResult : class, IUseCaseResult
        where TValidator : IUseCaseRequestValidator<TRequest>, new()
    {
        /// <summary>
        /// Метод реализации обработки UseCase
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected abstract TResult Handler(TRequest request);

        /// <summary>
        /// Метод проверки поступающего запроса на корректность
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected ValidationResult CheckValidity(TRequest request)
        {
            TValidator validator = new TValidator();
            var validation = validator.Validate(request);
            var errors = validation.Errors;
            Dictionary<string, List<string>> validationMessages = new Dictionary<string, List<string>>();
            foreach (var singleError in errors)
            {
                if (validationMessages.ContainsKey(singleError.PropertyName))
                {
                    List<string> propertyErrors = validationMessages[singleError.PropertyName];
                    propertyErrors.Add(singleError.ErrorMessage);
                }
                else
                {
                    validationMessages.Add(singleError.PropertyName, new List<string> { singleError.ErrorMessage });
                }
            }
            return new ValidationResult { Errors = validationMessages };
        }

        /// <summary>
        /// Проверка поступившего запроса на корректность и обработка этого запроса
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UseCaseResponse<TResult> Handle(TRequest request)
        {
            ValidationResult validationResult = CheckValidity(request);
            if (validationResult.IsValid)
            {
                try
                {
                    TResult useCaseResult = Handler(request);
                    return new UseCaseResponse<TResult>(true, useCaseResult);
                }
                catch (Exception exception)
                {
                    return new UseCaseResponse<TResult>(exception);
                }
            }
            else
            {
                return new UseCaseResponse<TResult>(new ValidationException(validationResult.Errors));
            }
        }

        /// <summary>
        /// Доступная обертка метода проверки поступающего запроса на корректность
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IUseCaseResponse<ValidationResult> Validate(TRequest request)
        {
            ValidationResult validationResult = CheckValidity(request);
            return new UseCaseResponse<ValidationResult>(validationResult.IsValid, validationResult);
        }
    }
}
