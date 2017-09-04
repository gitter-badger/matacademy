using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MatOrderingService2.Config;
using Microsoft.Extensions.Options;

namespace MatOrderingService2.Services.Storage.Impl
{
    public class CodeGenerationService : ICodeGenerationService
    {
        private readonly HttpClientSettings _options;

  public CodeGenerationService(IOptions<HttpClientSettings> options)
        {
            _options = options.Value;
        }

        public async Task<string> GetCodeForOrder(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri
                    (_options.BaseUrl); //config
                var contentType =
                    new MediaTypeWithQualityHeaderValue(_options.ContentType);
                client.DefaultRequestHeaders.Accept.Add(contentType);

                var response = await client.GetAsync(_options.Path + id);
                var stringData = response.Content.ReadAsStringAsync().Result;
                return stringData;
            }
        }
    }
}
