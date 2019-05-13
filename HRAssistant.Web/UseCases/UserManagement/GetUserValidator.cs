using FluentValidation;
using HRAssistant.Web.Contracts.UserManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.UserManagement
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
                        .MustAsync((userId, token) => userRepository.Exists(userId.Value))
                        .WithMessage("User with same ID was not found.");
                });
        }
    }
}
