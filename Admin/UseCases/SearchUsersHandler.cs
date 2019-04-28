using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HRAssistant.Admin.Contracts;
using HRAssistant.DataAccess.Core;
using HRAssistant.Infrastructure;
using HRAssistant.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Admin.UseCases
{
    internal sealed class SearchUsersHandler : IQueryHandler<SearchUsers, SearchUsersResult>
    {
        private readonly IUserRepository _userRepository;

        public SearchUsersHandler(IUserRepository userRepository)
        {
            Guard.AgainstNullArgument(nameof(userRepository), userRepository);

            _userRepository = userRepository;
        }

        public async Task<SearchUsersResult> Handle(SearchUsers request)
        {
            return await _userRepository.Search()
                .FilterBy(request.Username, u => u.Username.Contains(request.Username))
                .FilterBy(request.DisplayName, u => (u.FirstName + " " + u.LastName).Contains(request.DisplayName))
                .FilterBy(request.Role?.CreateDomain(), u => u.Role == request.Role.Value.CreateDomain())
                .FilterBy(request.Blocked, u => u.IsBlocked == request.Blocked)
                .Select(u => new SearchUserItem
                {
                    UserId = u.Id,
                    DisplayName = u.FirstName + " " + u.LastName,
                    Role = u.Role.ConvertToContract(),
                    Username = u.Username,
                    Blocked = u.IsBlocked
                })
                .ToSearchResults(request);
        }
    }
}
