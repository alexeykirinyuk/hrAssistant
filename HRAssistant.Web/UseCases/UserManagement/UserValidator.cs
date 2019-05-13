using FluentValidation;
using HRAssistant.Web.Contracts.UserManagement;

namespace HRAssistant.Web.UseCases.UserManagement
{
    public sealed class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Username).NotNull().NotEmpty();
            RuleFor(u => u.FirstName).NotNull().NotEmpty();
            RuleFor(u => u.LastName).NotNull().NotEmpty();
            RuleFor(u => u.Password).NotNull().NotEmpty();
            RuleFor(u => u.Role).NotNull();
            RuleFor(u => u.IsBlocked).NotNull();
        }
    }
}
