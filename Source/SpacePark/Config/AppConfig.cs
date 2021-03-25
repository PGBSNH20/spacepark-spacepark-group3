using System.IO;
using Microsoft.Extensions.Configuration;

namespace SpacePark.Config
{
    public class AppConfig
    {

        public string ConnectionString { get; set; }
        public string APIUrl { get; set; }
        public string Name { get; set; }

        public AppConfig GetConfig() 
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false, true)
                .Build();

            AppConfig programConfig = new AppConfig();

            config.GetSection("Settings").Bind(programConfig);

            programConfig.ConnectionString = config.GetConnectionString("Default");
            return programConfig;
        }
    }
}