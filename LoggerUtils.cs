using Serilog;
using Serilog.Enrichers;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using System.Linq;

namespace WebApiPruebaAlpha
{
    public static class LoggerUtils
    {
        public static void Init()
        {
            const string loggerTemplate = @"{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u4}]<{ThreadId}> [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";
            string baseDir = Config.Instance.LogPath;
            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }
            var entryAssemblyName = Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location);
            var logFileName = Path.Combine(baseDir, entryAssemblyName + ".log");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.With(new ThreadIdEnricher())
                .Enrich.FromLogContext()
                .WriteTo.Console(LogEventLevel.Information, loggerTemplate, theme: AnsiConsoleTheme.Literate)
                .WriteTo.File(logFileName, LogEventLevel.Information, loggerTemplate,
                    rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90)
                .CreateLogger();
        }

        public static void LogExecution(Action program)
        {
            var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                var basePath = Path.GetDirectoryName(entryAssembly.Location);
                Log.Information("====================================================================");
                Log.Information($"Application starts. Version: {entryAssembly.GetName().Version}");
                Log.Information($"Application directory: {AppDomain.CurrentDomain.BaseDirectory}");
                Log.Information($"Entry assembly: {entryAssembly}");
                Log.Information($"Entry assembly directory: {basePath}");
                Log.Information("[");
                var startIndex = basePath.Length + 1;
                var fileList = Directory.GetFiles(basePath, "*.*", SearchOption.TopDirectoryOnly)
                               .Select(x => x.Substring(startIndex))
                               .OrderBy(x => x);
                foreach (var file in fileList)
                {
                    Log.Information($"    {file}");
                }
                Log.Information("]");
                Log.Information("appsettings.json");
                Log.Information("[");
                var configurations = Config.Instance.getAll();
                foreach (var line in configurations)
                {
                    Log.Information($"    {line}");
                }
                Log.Information("]");
            }
            try
            {
                program();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Application terminated unexpectedly");
            }
            finally
            {
                Log.Information("====================================================================\r\n");
                Log.CloseAndFlush();
            }
        }
    }
}
