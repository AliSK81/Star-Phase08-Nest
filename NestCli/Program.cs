using NestCli.Services;

namespace NestCli
{
    public class Program
    {
        public static async Task Main()
        {
            //await HttpService.Instance.Run();

            NestService.Instance.CreateIndex();
            NestService.Instance.Run();
        }
    }
}