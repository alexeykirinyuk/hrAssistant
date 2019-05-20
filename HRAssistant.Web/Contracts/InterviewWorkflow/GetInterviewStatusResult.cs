using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRAssistant.Web.Contracts.InterviewWorkflow
{
    public sealed class GetInterviewStatusResult
    {
        public InterviewStatus Status { get; set; }
    }
}