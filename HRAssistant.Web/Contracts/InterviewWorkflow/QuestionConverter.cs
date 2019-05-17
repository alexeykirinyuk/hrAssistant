using System;
using HRAssistant.Web.Contracts.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class QuestionConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Question);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var type = jsonObject["questionType"].ToObject<QuestionType>();
            Shared.Question question = CreateQuestion(type);

            serializer.Populate(jsonObject.CreateReader(), question);

            return question;
        }

        private static Shared.Question CreateQuestion(QuestionType type)
        {
            switch (type)
            {
                case QuestionType.Input:
                    return new Shared.InputQuestion();
                case QuestionType.Select:
                    return new Shared.SelectQuestion();
                case QuestionType.General:
                    return new JobPositionManagement.GeneralQuestion();
                default:
                    throw new NotSupportedException($"Unknown question type: {type}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
}