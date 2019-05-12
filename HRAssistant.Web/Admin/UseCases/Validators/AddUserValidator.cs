using FluentValidation;
using HRAssistant.Web.Admin.Contracts.UserContracts;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.Admin.UseCases.Validators
{
    internal sealed class AddUserValidator : AbstractValidator<AddUser>
    {
        public AddUserValidator(IUserRepository userRepository)
        {
            RuleFor(u => u.User)
                .NotNull()
                .DependentRules(() =>
                {
                    RuleFor(u => u.User.Id).Null()
                        .WithMessage("{PropertyName} should be null when creating new user.");

                    RuleFor(u => u.User.Username)
                        .MustAsync(async (username, token) => !await userRepository.ExistsByUsername(username))
                        .WithMessage("User with the same name '{PropertyValue}' should not exists.");

                    RuleFor(u => u.User).SetValidator(new UserValidator());
                });
        }
    }
}
