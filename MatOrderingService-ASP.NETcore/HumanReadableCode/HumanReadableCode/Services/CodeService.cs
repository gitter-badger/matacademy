using System;
using System.Collections;
using System.IO;
using HumanReadableCode.Configs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace HumanReadableCode.Services
{
    public class CodeService
    {
        private readonly CodeGenerationSettings _options;

        private Random _rnd = new Random();

        public CodeService(IOptions<CodeGenerationSettings> options)
        {
            _options = options.Value;
        }

        private string GetRandomValue()
        {
            char[] code = {
                _options.Consonants[_rnd.Next(0, 20)],
                _options.Vowels[_rnd.Next(0, 4)],
                _options.Consonants[_rnd.Next(0, 20)],
                _options.Vowels[_rnd.Next(0, 4)],
                _options.Consonants[_rnd.Next(0, 20)],
                _options.Vowels[_rnd.Next(0, 4)]
            };
            return new string(code);
        }

        private void GenerateCodes()
        {
            var data = new Hashtable();

            using (var file = File.CreateText(string.Format("{0}/wwwroot/CodeDatabase/file.json", Directory.GetCurrentDirectory())))
            {

                for (var i = 1; i <= 100; i++)
                {
                    var value = GetRandomValue();
                    while (data.ContainsValue(value))
                    {
                        value = GetRandomValue();
                    }
                    data.Add(i, value);
                }
                var serializer = new JsonSerializer();
                serializer.Serialize(file, data);
            }
        }

        public string GetCodeForId(int id)
        {
            if (!File.Exists(string.Format("{0}/wwwroot/CodeDatabase/file.json", Directory.GetCurrentDirectory())))
                GenerateCodes();
            using (var r = File.OpenText(string.Format("{0}/wwwroot/CodeDatabase/file.json", Directory.GetCurrentDirectory())))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Hashtable>(json);
                var codeId = id % 100;
                return string.Format(_options.Prefix, DateTime.Now) + "-" + items[codeId.ToString()];
            }
        } 
    }
}
