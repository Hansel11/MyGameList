using System;

namespace MyGameList.Domain.Request
{
    public class GameRequestDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public string Genre { get; set; }
    }
}
