using HRAssistant.Infrastructure;

namespace HRAssistant.Admin.Contracts.UserContracts
{
    public sealed class SearchUsers : SearchRequest<SearchUsersResult>
    {
        public string Username { get; set; }

        public string DisplayName { get; set; }

        public Role? Role { get; set; }

        public bool? Blocked { get; set; }
    }
}
