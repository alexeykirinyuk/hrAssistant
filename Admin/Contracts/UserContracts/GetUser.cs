using HRAssistant.Infrastructure.CQRS;
using System;

namespace HRAssistant.Admin.Contracts
{
    public sealed class GetUser : IQuery<GetUserResult>
    {
        public Guid? UserId { get; set; }
    }
}
