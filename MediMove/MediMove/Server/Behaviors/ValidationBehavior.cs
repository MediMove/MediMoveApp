using FluentValidation;
using MediatR;
using ErrorOr;

namespace MediMove.Server.Behaviors
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : IErrorOr
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));

                var errors = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList().ConvertAll(
                    validationFailure => Error.Validation(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage));

                if (errors.Any())
                    return (dynamic)errors;
            }
            return await next();
        }
    }
}
