using System;
using HRAssistant.Web.Contracts.JobPositionManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HRAssistant.Web.Contracts.Shared.Converters
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
            Question question = CreateQuestion(type);

            serializer.Populate(jsonObject.CreateReader(), question);

            return question;
        }

        private static Question CreateQuestion(QuestionType type)
        {
            switch (type)
            {
                case QuestionType.Input:
                    return new InputQuestion();
                case QuestionType.Select:
                    return new SelectQuestion();
                case QuestionType.General:
                    return new GeneralQuestion();
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
