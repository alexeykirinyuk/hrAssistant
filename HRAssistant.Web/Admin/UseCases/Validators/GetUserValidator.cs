﻿using FluentValidation;
using HRAssistant.Admin.Contracts;
using HRAssistant.Admin.Contracts.UserContracts;
using HRAssistant.DataAccess.Core;

namespace HRAssistant.Admin.UseCases.Validators
{
    internal sealed class GetUserValidator : AbstractValidator<GetUser>
    {
        public GetUserValidator(IUserRepository userRepository)
        {
            RuleFor(request => request.UserId)
                .NotNull()
                .NotEmpty()
                .DependentRules(() =>
                {
                    RuleFor(request => request.UserId)
                        .MustAsync((userId, token) => userRepository.Exists(userId.Value))
                        .WithMessage("User with same ID was not found.");
                });
        }
    }
}