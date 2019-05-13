using HRAssistant.Web.Contracts.Shared.Converters;
using Newtonsoft.Json;

namespace HRAssistant.Web.Contracts.Shared
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
