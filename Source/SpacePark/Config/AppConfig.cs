using System.IO;
using Microsoft.Extensions.Configuration;

namespace SpacePark.Config
{
    public class AppConfig
    {
        public string ConnectionString { get; set; }
        public string APIUrl { get; set; }
        public string Name { get; set; }

        public static AppConfig GetConfig()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false, true)
                .Build();

            AppConfig programConfig = new();

            config.GetSection("Settings").Bind(programConfig);

            programConfig.ConnectionString = config.GetConnectionString("Default");
            return programConfig;
        }
    }
}