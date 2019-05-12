using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.UserContracts;
using HRAssistant.Web.Admin.UseCases.Mapping;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class UpdateUserHandler : ICommandHandler<UpdateUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(userRepository), userRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateUser request)
        {
            var userEntity = await _userRepository.Get(request.User.Id.Value);

            userEntity.Update(request.User);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
