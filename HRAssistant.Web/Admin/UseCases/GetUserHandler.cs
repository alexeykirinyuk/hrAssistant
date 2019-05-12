using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.UserContracts;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class GetUserHandler : IQueryHandler<GetUser, GetUserResult>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            Guard.AgainstNullArgument(nameof(userRepository), userRepository);

            _userRepository = userRepository;
        }

        public async Task<GetUserResult> Handle(GetUser request)
        {
            var entity = await _userRepository.Get(request.UserId.Value);

            return new GetUserResult
            {
                User = entity.ConvertToContract()
            };
        }
    }
}
