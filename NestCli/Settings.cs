using System.Reflection;

namespace NestCli
{
    public class Settings
    {
        public static readonly string Uri = "http://localhost:9200/";
        public static readonly string Index = "people-proj";
        public static readonly string DatasetPath = Assembly.GetExecutingAssembly().Location + @"\..\Resources\people.json";
    }
}
