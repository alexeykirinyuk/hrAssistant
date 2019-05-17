using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRAssistant.Web.Contracts.Shared;

namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class InputQuestion : Question
    {
        public override QuestionType QuestionType => QuestionType.Input;
    }
}
