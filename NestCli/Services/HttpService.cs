using NestCli.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
namespace NestCli.Services
{
    public class HttpService
    {
        private static HttpService? _instance;

        readonly HttpClient client = new();

        private HttpService()
        {
            client.BaseAddress = new Uri(Settings.Uri);
            client.DefaultRequestHeaders.Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static HttpService Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new HttpService();
                }
                return _instance;
            }
        }


        public async Task Run()
        {
            var peopleList = JsonReader.Instance.ReadJsonList<Person>(Settings.DatasetPath);

            try
            {
                foreach (var person in peopleList)
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, Settings.Index + "/_doc");
                    request.Content = new StringContent(JsonConvert.SerializeObject(person),
                                                        Encoding.UTF8,
                                                        "application/json");

                    await client.SendAsync(request)
                          .ContinueWith(responseTask =>
                          {
                              Console.WriteLine("Response: {0}", responseTask.Result);
                          });
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
