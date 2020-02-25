using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace PwcBios.Api.Infrastructure
{
    public interface IApiConfig
    {
        string PvcBiosApiConnectionString { get; }
    }
    public class ApiConfig : IApiConfig
    {
        private readonly IConfiguration _configuration;
        public ApiConfig(string environment)
        {
            _configuration = GetConfiguration(environment);
        }

        private IConfigurationRoot GetConfiguration(string environment)
        {
            var basePath = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            if (environment.Equals("local", StringComparison.InvariantCultureIgnoreCase))
            {
                builder.AddJsonFile($"appsettings.{environment}.json", optional: true);
            }

            return builder.Build();
        }
        public string PvcBiosApiConnectionString => _configuration.GetConnectionString("PvcBiosApiConnectionString");
    }
}
