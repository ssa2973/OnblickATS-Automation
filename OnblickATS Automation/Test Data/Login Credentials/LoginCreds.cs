using System;
using System.IO;
using System.Text.Json;

namespace OnblickATS_Automation
{
    public class LoginCreds
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private static readonly Lazy<LoginCreds> _instance = new Lazy<LoginCreds>(() => new LoginCreds());

        private readonly JsonElement _envConfig;

        private LoginCreds()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var filePath = Path.Combine(currentDirectory, "..", "..", "..", "Test Data", "Login Credentials", "creds.json");
            var configText = File.ReadAllText(filePath);
            var configJson = JsonDocument.Parse(configText);
            configJson.RootElement.TryGetProperty(Environment.Name, out _envConfig);
        }

        //Add LoginCreds in the json file and then call them here
        public static LoginCreds Instance => _instance.Value;
        public string HR_Email => _envConfig.GetProperty("HR_Email").GetString();
        public string HR_Pwd => _envConfig.GetProperty("HR_Pwd").GetString();
    }

}

