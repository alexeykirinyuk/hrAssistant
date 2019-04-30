using FluentValidation;
using HRAssistant.Admin.Contracts;
using HRAssistant.Admin.Contracts.UserContracts;
using HRAssistant.DataAccess.Core;

namespace HRAssistant.Admin.UseCases.Validators
{
    internal sealed class UpdateUserValidator : AbstractValidator<UpdateUser>
    {
        public UpdateUserValidator(IUserRepository userRespository)
        {
            RuleFor(m => m.User).NotNull()
                .DependentRules(() =>
                {
                    RuleFor(m => m.User.Id).NotNull()
                        .MustAsync((id, token) => userRespository.Exists(id.Value))
                        .WithMessage("User with id '{PropertyValue}' should be exists.");
                    RuleFor(m => m.User)
                        .MustAsync(async (user, token) => !await userRespository.ExistsByUsername(user.Username, user.Id))
                        .WithMessage("User with the same Username already exists.");

                    RuleFor(m => m.User).SetValidator(new UserValidator());
                });
        }
    }
}
