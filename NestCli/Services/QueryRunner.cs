using Nest;
using NestCli.Models;

namespace NestCli.Services
{
    public class QueryRunner
    {
        private readonly ElasticClient _client;

        public QueryRunner(ElasticClient client)
        {
            _client = client;
        }

        public void BoolQuery()
        {
            var response = _client.Search<Person>(s => s
                .Index(Settings.Index)
                .Query(q => q
                    .Bool(b => b
                        .Must(must => must
                            .Match(match => match
                                .Field(p => p.About)
                                .Query("Labore"))))));

            Console.WriteLine(response.Hits);
        }

        public void MatchQuery()
        {
            var response = _client.Search<Person>(s => s
                .Index(Settings.Index)
                .Query(q => q
                    .Match(m => m
                        .Name("mohammad"))));

            Console.WriteLine(response.DebugInformation);
        }

        public void FuzzyQuery()
        {
            var response = _client.Search<Person>(s => s
                .Index(Settings.Index)
                .Query(q => q
                    .Match(m => m
                        .Field(p => p.Name)
                        .Query("Contreras")
                        .Fuzziness(Fuzziness.Auto))));

            Console.WriteLine(response.DebugInformation);
        }
        public void RangeQuery()
        {
            var response = _client.Search<Person>(s => s
              .Index(Settings.Index)
              .Query(q => q
                  .Range(r => r
                      .Field(p => p.Age)
                        .GreaterThanOrEquals(24)
                        .LessThanOrEquals(35))));

            Console.WriteLine(response.DebugInformation);
        }

        public void TermQuery()
        {
            var response = _client.Search<Person>(s => s
                .Index(Settings.Index)
                .Query(q => q
                    .Term(t => t
                        .Field(p => p.Email)
                            .Value("lolajefferson@buzzmaker.com"))));

            Console.WriteLine(response.DebugInformation);

        }

        public void MutliMatchQuery()
        {
            var response = _client.Search<Person>(s => s
                .Index(Settings.Index)
                .Query(q => q
                    .MultiMatch(mm => mm
                        .Query("Lola")
                        .Fields(Infer.Fields<Person>(p => p.Name, p => p.Email))
                        .Fuzziness(Fuzziness.Auto))));

            Console.WriteLine(response.DebugInformation);
        }
    }
}