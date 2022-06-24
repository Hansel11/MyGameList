using System;

namespace MyGameList.Domain.Response
{
    public class GameResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public string Genre { get; set; }
    }
}
