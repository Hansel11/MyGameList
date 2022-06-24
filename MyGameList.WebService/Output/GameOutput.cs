using MyGameList.Domain.Response;
using System.Collections.Generic;

namespace MyGameList.WebService.Output
{
    public class GameOutput : OutputBase
    {
        public List<GameResponseDTO> Games{ get; set; }

        public GameOutput()
        {
            this.Games = new List<GameResponseDTO>();
        }
    }
}
