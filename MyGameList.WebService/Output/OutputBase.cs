using System;

namespace MyGameList.WebService.Output
{
    public class OutputBase
    {
        public int ResultCode { get; set; }

        public string ErrorMessage { get; set; }

        public OutputBase()
        {
            ResultCode = 200;
            ErrorMessage = "Success";
        }

        public OutputBase(Exception ex)
        {
            ResultCode = 500;
            ErrorMessage = ex.Message;
        }
    }
}
