using NestCli.Services;

namespace NestCli
{
    public class Program
    {
        public static async Task Main()
        {
            //await HttpService.Instance.Run();

            //NestService.Instance.CreateIndex();
            //NestService.Instance.Run();

            var runner = new QueryRunner(NestService.Instance.client);
            //runner.BoolQuery();
            //runner.MatchQuery();
            //runner.FuzzyQuery();
            //runner.RangeQuery();
            //runner.TermQuery();
            runner.MutliMatchQuery();
        }
    }
}