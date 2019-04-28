using System;
using System.Threading;
using System.Threading.Tasks;
using HRAssistant.Admin.Contracts;
using HRAssistant.DataAccess.Core;
using HRAssistant.Domain;
using HRAssistant.Infrastructure.CQRS;
using LiteGuard;

namespace HRAssistant.Admin.UseCases
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
