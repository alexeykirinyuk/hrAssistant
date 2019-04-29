using HRAssistant.Admin.Contracts.JobPositionContracts.Converters;
using Newtonsoft.Json;
using System;

namespace HRAssistant.Admin.Contracts.JobPositionContracts
{
    [JsonConverter(typeof(QuestionConverter))]
    public abstract class Question
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int? OrderIndex { get; set; }

        public abstract QuestionType QuestionType { get; }
    }
}
