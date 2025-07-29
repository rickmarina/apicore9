using System.Text;
using System.Text.Json;

namespace com.rorisoft.net
{
    public class WebAPIUtils
    {
        /// <summary>
        /// Post API data to Uri
        /// Example using Model Class: var res = postAPI<InvoiceData, Payload_Test>("VettingVetRequest/GetInvoiceData", new Payload_Test() { IdCost = 2222 }).Result;
        /// Example using Json string object: var res = postAPI<InvoiceData, Object>("VettingVetRequest/GetInvoiceData", sr.DeserializeObject("{IdCost:2222}")).Result;
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <typeparam name="TPost"></typeparam>
        /// <param name="uriAction"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<TRes> postAPI<TRes, TPost>(string uriAction, TPost data, string urlBase = "")
        {
            TRes res = default(TRes)!;

            try
            {
                var url = urlBase + uriAction;

                using (var client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true, UseProxy = false }))
                {
                    var req = new HttpRequestMessage(HttpMethod.Post, url);


                    var json = JsonSerializer.Serialize(data);
                    var dataPost = new StringContent(json, Encoding.UTF8, "application/json");
                    req.Content = dataPost;

                    var response = client.SendAsync(req).Result;
                    var result = await response.Content.ReadAsStringAsync();

                    res = JsonSerializer.Deserialize<TRes>(result)!;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return res;

        }
    }
}
