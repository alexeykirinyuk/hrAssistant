using System.Text.RegularExpressions;
using FluentValidation;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class SetContactInformationValidator : AbstractValidator<SetContactInformation>
    {
        private Regex _validPhoneRegex = new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}");

        public SetContactInformationValidator(IVacancyRepository vacancyRepository)
        {
            RuleFor(m => m.VacancyId).NotNull()
                .WithMessage(Messages.NotNull)
                .DependentRules(() =>
                {
                    RuleFor(m => m.VacancyId)
                        .MustAsync((id, token) => vacancyRepository.Exists(id.Value))
                        .WithMessage("Вакансии с Id '{PropertyValue}' не существует.");
                });

            RuleFor(m => m.Email).EmailAddress()
                .WithMessage("Email адрес невалидный.");

            RuleFor(m => m.Phone).Must(_validPhoneRegex.IsMatch)
                .WithMessage("Телефон невалидный.");
        }
    }
}
