using Newtonsoft.Json;
using System.Text;

namespace MyGameList.Helper
{
    public class HttpHelper
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<HttpResponseMessage> Post(string baseUrl, dynamic data)
        {
            var req = JsonConvert.SerializeObject(data);
            try
            {
                string json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return await client.PostAsync(baseUrl, content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
