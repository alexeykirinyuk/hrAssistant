using System;
using System.Threading.Tasks;
using HRAssistant.Web.Admin.Contracts.UserContracts;
using HRAssistant.Web.Admin.UseCases.Mapping;
using HRAssistant.Web.DataAccess.Core;
using HRAssistant.Web.Domain;
using HRAssistant.Web.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Web.Admin.UseCases
{
    internal sealed class AddUserHandler : ICommandHandler<AddUser, AddUserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            Guard.AgainstNullArgument(nameof(userRepository), userRepository);
            Guard.AgainstNullArgument(nameof(unitOfWork), unitOfWork);

            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddUserResult> Handle(AddUser request)
        {
            var userId = Guid.NewGuid();

            var entity = new UserEntity { Id = userId };
            entity.Update(request.User);

            _userRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            return new AddUserResult { UserId = userId };
        }
    }
}
