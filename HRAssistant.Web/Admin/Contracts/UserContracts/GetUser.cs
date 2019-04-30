using System;
using HRAssistant.Infrastructure.CQRS;

namespace HRAssistant.Admin.Contracts.UserContracts
{
    public sealed class GetUser : IQuery<GetUserResult>
    {
        public Guid? UserId { get; set; }
    }
}
