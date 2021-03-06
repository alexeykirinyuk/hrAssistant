﻿using System;
using HRAssistant.Web.Contracts.UserManagement;
using HRAssistant.Web.Domain;
using LiteGuard;

namespace HRAssistant.Web.UseCases.UserManagement
{
    public static class UserOperationExtensions
    {
        public static void Update(this UserEntity userEntity, User user)
        {
            Guard.AgainstNullArgument(nameof(userEntity), userEntity);
            Guard.AgainstNullArgument(nameof(user), user);

            userEntity.Username = user.Username;
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;
            userEntity.Password = user.Password;
            userEntity.Role = user.Role.Value.CreateDomain();
            userEntity.IsBlocked = user.IsBlocked.Value;
        }

        public static RoleEntity CreateDomain(this Role role)
        {
            Guard.AgainstNullArgumentIfNullable(nameof(role), role);

            switch (role)
            {
                case Role.HR:
                    return RoleEntity.HR;
                case Role.TeamLead:
                    return RoleEntity.TeamLead;
                default:
                    throw new NotSupportedException($"Role '{role}' is not supported.");
            }
        }

        public static User ConvertToContract(this UserEntity entity)
        {
            Guard.AgainstNullArgument(nameof(entity), entity);

            return new User
            {
                Id = entity.Id,
                Username = entity.Username,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Password = entity.Password,
                Role = entity.Role.ConvertToContract(),
                IsBlocked = entity.IsBlocked
            };
        }

        public static Role ConvertToContract(this RoleEntity entity)
        {
            Guard.AgainstNullArgumentIfNullable(nameof(entity), entity);

            switch (entity)
            {
                case RoleEntity.HR:
                    return Role.HR;
                case RoleEntity.TeamLead:
                    return Role.TeamLead;
                default:
                    throw new NotSupportedException($"RoleEntity '{entity}' is not supported.");
            }
        }
    }
}
