using HRAssistant.Admin.Contracts;
using HRAssistant.DataAccess.Core;
using HRAssistant.Infrastructure.CQRS;
using LiteGuard;
using System.Threading;
using System.Threading.Tasks;
using HRAssistant.Admin.Contracts.UserContracts;

namespace HRAssistant.Admin.UseCases
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
