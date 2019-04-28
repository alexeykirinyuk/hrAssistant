using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAssistant.Infrastructure
{
    public class Validator<TRequest>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public Validator(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task Validate(TRequest request)
        {
            var context = new ValidationContext(request);
            var validateTasks = _validators
                .Select(v => v.ValidateAsync(context))
                .ToArray();

            await Task.WhenAll(validateTasks);

            var errors = validateTasks
                .SelectMany(t => t.Result.Errors)
                .ToArray();

            if (errors.Length != 0)
            {
                throw new ValidationException(errors);
            }
        }
    }
}
