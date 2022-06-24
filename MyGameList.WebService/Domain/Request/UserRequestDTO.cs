using System;

namespace MyGameList.Domain.Request
{
    public class UserRequestDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
