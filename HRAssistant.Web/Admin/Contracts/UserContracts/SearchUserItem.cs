using System;

namespace HRAssistant.Admin.Contracts.UserContracts
{
    public sealed class SearchUserItem
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string DisplayName { get; set; }

        public Role Role { get; set; }

        public bool Blocked { get; set; }
    }
}
