using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace WebApiPruebaAlpha
{
    public interface IConfig
    {
        string ConnectionString { get; }

        string LogPath { get; }

        int PollingIntervalInSeconds { get; }

        List<string> getAll();
    }

    public class Config : IConfig
    {
        //
        // Summary:
        //     The globally-shared Config instance.
        public static IConfig Instance = new Config();

        private IConfiguration _configuration;

        public Config()
        {
            var basePath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", false, true);
            _configuration = builder.Build();
        }

        public string ConnectionString => _configuration.GetConnectionString("RepositorioAlphaMVM").ToString();

        public string LogPath => _configuration.GetSection("RepositorioAlphaMVM:LogPath").Value.ToString();

        public int PollingIntervalInSeconds
        {
            get
            {
                int result = 0;
                int.TryParse(_configuration.GetSection("RepositorioAlphaMVM:PollingIntervalInSeconds").Value, out result);
                return result;
            }
        }

        public List<string> getAll()
        {
            var result = new List<string>();
            result.Add($"ConnectionString: {ConnectionString}");
            result.Add($"LogPath: {LogPath}");
            result.Add($"PollingIntervalInSeconds: {PollingIntervalInSeconds}");
            return result;
        }
    }
}
