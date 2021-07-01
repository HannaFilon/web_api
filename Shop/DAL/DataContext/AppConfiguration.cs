using System.IO;
using Microsoft.Extensions.Configuration;

namespace DAL.DataContext
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
        }

        public string sqlConnectionString { get; set; }
    }
}
