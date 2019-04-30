namespace HRAssistant.Admin.Contracts.JobPositionContracts
{
    public sealed class Template
    {
        public string Description { get; set; }

        public Question[] Questions { get; set; }
    }
}
