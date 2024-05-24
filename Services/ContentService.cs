using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DominicoBus.Services
{
    public class ContentService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _environment;
        private readonly Dictionary<string, Dictionary<string, string>>? _content;

        public ContentService(IConfiguration configuration, IHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
            _content = LoadContent();
        }

        private Dictionary<string, Dictionary<string, string>>? LoadContent()
        {
            // TODO: Support multiple languages
            var path = Path.Combine(_environment.ContentRootPath, "wwwroot/contents/es.json");
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);
        }

        public string GetContent(string page, string key)
        {
            if (_content is not null && _content.ContainsKey(page) && _content[page].TryGetValue(key, out string? value))
            {
                return value;
            }

            return String.Empty;
        }
    }
}