using System;
using HRAssistant.Web.Infrastructure.CQRS;

namespace HRAssistant.Web.Admin.Contracts.UserContracts
{
    public sealed class GetUser : IQuery<GetUserResult>
    {
        public Guid? UserId { get; set; }
    }
}
