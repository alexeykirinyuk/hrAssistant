namespace HRAssistant.Web.Contracts.InterviewList
{
    public sealed class GetInterviewResult
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string TeamTitle { get; set; }

        public string JobPositionTitle { get; set; }

        public string CityTitle { get; set; }

        public int CorrectAnswersCount { get; set; }

        public int IncorrectAnswersCount { get; set; }

        public Question[] Questions { get; set; }
    }
}
