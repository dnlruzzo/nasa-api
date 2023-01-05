using FluentValidation;
using MediatR;

namespace Nasa.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<TRequest>(request);

        var validateTasks = _validators
            .Select(x => x.ValidateAsync(validationContext, cancellationToken));

        var validationResults = await Task.WhenAll(validateTasks);

        var failures = validationResults
            .Where(x => x.Errors.Any())
            .SelectMany(x => x.Errors)
            .ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
