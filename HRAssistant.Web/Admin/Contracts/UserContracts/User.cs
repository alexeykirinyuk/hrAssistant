using System;

namespace HRAssistant.Admin.Contracts.UserContracts
{
    public sealed class User
    {
        public Guid? Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role? Role { get; set; }

        // TODO: добавить шифрование.
        public string Password { get; set; }

        public bool? IsBlocked { get; set; }
    }
}
