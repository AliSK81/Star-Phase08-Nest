using Nest;
using NestCli.Models;

namespace NestCli.Services
{
    public class NestService
    {
        private static NestService? _instance;

        public ElasticClient client { get; }
        public static NestService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NestService();
                }
                return _instance;
            }
        }
        private NestService()
        {
            var uri = new Uri(Settings.Uri);
            var connectionSettings = new ConnectionSettings(uri);

            connectionSettings.EnableDebugMode();
            client = new ElasticClient(connectionSettings);

            var response = client.Ping();
            Console.WriteLine(response);
        }

        public void CreateIndex()
        {
            var response = client.Indices.Create(Settings.Index,
                    s => s.Map<Person>(m => m
                                .Properties(pr => pr
                                    .Number(t => t.Name(n => n.Age))
                                    .Text(t => t.Name(n => n.EyeColor))
                                    .Text(t => t.Name(n => n.Name))
                                    .Text(t => t.Name(n => n.Gender))
                                    .Text(t => t.Name(n => n.Company))
                                    .Text(t => t.Name(n => n.Email))
                                    .Text(t => t.Name(n => n.Phone))
                                    .Text(t => t.Name(n => n.Address))
                                    .Text(t => t.Name(n => n.About))
                                    .Date(t => t
                                        .Name(n => n.RegistrationDate)
                                        .Format("YYYY/MM/dd HH:mm:ss"))
                                    .GeoPoint(t => t.Name(n => n.Location)))));

            Console.WriteLine(response);
        }

        public void Run()
        {
            var peopleList = JsonReader.Instance.ReadJsonList<Person>(Settings.DatasetPath);

            try
            {
                var bulkDescriptor = new BulkDescriptor();
                foreach (var person in peopleList)
                {

                    //Console.WriteLine(JsonConvert.SerializeObject(person, Formatting.Indented));
                    bulkDescriptor.Index<Person>(x => x
                        .Index(Settings.Index)
                        .Document(person)
                    );
                }
                var response = client.Bulk(bulkDescriptor);
                Console.WriteLine(response);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

    }

}
