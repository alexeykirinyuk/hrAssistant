using FluentValidation;
using HRAssistant.Web.Contracts.UserManagement;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.UserManagement
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
