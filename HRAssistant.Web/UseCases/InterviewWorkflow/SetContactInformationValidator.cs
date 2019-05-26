using System.Text.RegularExpressions;
using FluentValidation;
using HRAssistant.Web.Contracts.InterviewWorkflow;
using HRAssistant.Web.DataAccess.Core;

namespace HRAssistant.Web.UseCases.InterviewWorkflow
{
    internal sealed class SetContactInformationValidator : AbstractValidator<SetContactInformation>
    {
        private readonly Regex _validPhoneRegex = new Regex(@"^([0-9]{11})$");

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

            RuleFor(m => m.TermsAgreed).Must(t => t == true)
                .WithMessage("Необходимо согласиться с правилами.");
        }
    }
}
