using System;

namespace HRAssistant.Domain
{
    public sealed class UserEntity
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public RoleEntity Role { get; set; }

        // TODO: добавить шифрование.
        public string Password { get; set; }

        public bool IsBlocked { get; set; }
    }
}
