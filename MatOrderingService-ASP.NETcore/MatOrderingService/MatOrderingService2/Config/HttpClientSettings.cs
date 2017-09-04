using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService2.Config
{
    public class HttpClientSettings
    {
        public string BaseUrl { get; set; }
        public string Path { get; set; }
        public string ContentType { get; set; }
    }
}
