using FluentValidation;
using HRAssistant.Admin.Contracts;
using HRAssistant.DataAccess.Core;

namespace HRAssistant.Admin.UseCases.Validators
{
    internal sealed class GetUserValidator : AbstractValidator<GetUser>
    {
        public GetUserValidator(IUserRepository userRepository)
        {
            RuleFor(request => request.UserId)
                .NotNull()
                .NotEmpty()
                .DependentRules(() =>
                {
                    RuleFor(request => request.UserId)
                        .MustAsync((userId, token) => userRepository.Exists(userId.Value));
                });
        }
    }
}
