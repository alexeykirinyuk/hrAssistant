using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Contracts.UserManagement
{
    public sealed class GetUser : IQuery<GetUserResult>
    {
        public Guid? UserId { get; set; }
    }
}
