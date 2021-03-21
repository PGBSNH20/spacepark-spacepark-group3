using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using SpacePark.Models;

namespace SpacePark.Configuration
{
    public class AppConfig
    {

        public Config GetConfig() 
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false, true)
                .Build();

            Config programConfig = new Config();

            config.GetSection("Settings").Bind(programConfig);

            programConfig.ConnectionString = config.GetConnectionString("Default");

            Console.WriteLine($"Config value {programConfig}, Name: {programConfig.Name}, URL: {programConfig.APIUrl}, Conn: {programConfig.ConnectionString}");

            return new SpacePark.Models.Config{};
        }
    }
}