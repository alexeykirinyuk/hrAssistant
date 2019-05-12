using HRAssistant.Web.Domain;

namespace HRAssistant.Web.DataAccess.Core
{
    public interface IVacancyRepository
    {
        void Add(VacancyEntity vacancy);
    }
}
