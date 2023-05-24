using System;
using System.IO;
using System.Text.Json;

namespace OnblickATS_Automation
{
    public class Environment
    {
        //Change Environment here
        public static string Name => System.Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "dev";
    }

    public class URLs
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private static readonly Lazy<URLs> _instance = new Lazy<URLs>(() => new URLs());

        private readonly JsonElement _envConfig;

        private URLs()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory,"..","..","..","Environment_URLs\\urls_config.json");
            var configText = File.ReadAllText(filePath);
            var configJson = JsonDocument.Parse(configText);
            configJson.RootElement.TryGetProperty(Environment.Name, out _envConfig);
        }
        public static URLs Instance => _instance.Value;
        public string Login_URL => _envConfig.GetProperty("LOGIN_URL").GetString();
        public string Dashboard_URL => _envConfig.GetProperty("DASHBOARD_URL").GetString();
        public string Hotlist_URL => _envConfig.GetProperty("HOTLIST_URL").GetString();
    }
}
