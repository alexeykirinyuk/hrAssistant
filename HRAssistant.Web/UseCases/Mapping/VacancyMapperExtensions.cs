using System;
using System.Linq;
using HRAssistant.Web.Contracts.VacancyManagement;
using HRAssistant.Web.Domain;
using HRAssistant.Web.UseCases.JobPositionManagement;

namespace HRAssistant.Web.UseCases.Mapping
{
    public static class VacancyMapperExtensions
    {
        public static void Update(this VacancyEntity entity, Vacancy vacancy)
        {
            entity.TeamId = vacancy.TeamId.Value;
            entity.JobPositionId = vacancy.JobPositionId.Value;
            entity.JobsNumber = vacancy.JobsNumber.Value;
            entity.Salary = vacancy.Salary;
            entity.CandidateRequirements = vacancy.CandidateRequirements;
            entity.Status = VacancyStatusEntity.Draft;
            entity.Form.Description = vacancy.Form.Description;
            
            entity.Form.Questions.Clear();
            entity.Form.Questions.AddRange(vacancy.Form.Questions.Select(q => q.CreateQuestionEntity()));
        }
        public static VacancyStatus CreateContract(this VacancyStatusEntity status)
        {
            switch (status)
            {
                case VacancyStatusEntity.Draft: return VacancyStatus.Draft;
                case VacancyStatusEntity.Opened: return VacancyStatus.Opened;
                case VacancyStatusEntity.Closed: return VacancyStatus.Closed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
        public static VacancyStatusEntity CreateDomain(this VacancyStatus status)
        {
            switch (status)
            {
                case VacancyStatus.Draft: return VacancyStatusEntity.Draft;
                case VacancyStatus.Opened: return VacancyStatusEntity.Opened;
                case VacancyStatus.Closed: return VacancyStatusEntity.Closed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}
