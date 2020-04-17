using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ExternalService.Models;
using Newtonsoft.Json;

namespace ExternalService.Connections.Helpers
{
    public static class ApiHelper
    {
        /// <summary>
        /// makes api call returns specified type object.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="accountId"></param>
        /// <param name="szContent"></param>
        /// <returns></returns>
        public static async Task<string> MakeAPICall(HttpClient client, HttpMethod method, string url,
            CancellationToken ctoken, StringContent szContent = null)
        {
            FaultDM fault;

            /* What could you do for better error handling?
               What about error logging?
            */

            try
            {
                //make call
                HttpResponseMessage response;
                if (method == HttpMethod.Post)
                    response = await client.PostAsync(url, szContent, ctoken);
                else if (method == HttpMethod.Put)
                    response = await client.PutAsync(url, szContent, ctoken);
                else if (method == HttpMethod.Get)
                    response = await client.GetAsync(url, ctoken);
                else
                {
                    return JsonConvert.SerializeObject(new FaultDM
                        {Code = 400, Message = "Invalid HttpMethod type specified in call to MakeAPICall"});
                }

                //verify if we got a result
                if (!response.IsSuccessStatusCode)
                {
                    fault = new FaultDM
                    {
                        Code = (int) response.StatusCode,
                        Message = $" Failure Response ({response.StatusCode}) from api call to {url}: " +
                                  response.AsFormattedString()
                    };
                    return (JsonConvert.SerializeObject(fault));
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                var message = $"Exception thrown while calling url {url}: {ex}";
                fault = new FaultDM {Code = 500, Message = message};
                return JsonConvert.SerializeObject(fault);
            }
        }
    }
}
