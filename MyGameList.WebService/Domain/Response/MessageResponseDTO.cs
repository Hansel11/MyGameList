using System;

namespace psdtest.Domain.Response
{
    public class MessageResponseDTO
    {
        public string Message { get; set; }

        public MessageResponseDTO()
        {
            Message = "Success";
        }

        public MessageResponseDTO(Exception ex)
        {
            Message = ex.Message;
        }
    }
}
