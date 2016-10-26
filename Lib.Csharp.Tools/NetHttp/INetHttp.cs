using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools.NetHttp
{
    public interface INetHttp
    {
        TimeSpan Timeout { get; set; }

        Task<string> GetStringAsync(string requestUri, Dictionary<string, string> headers);
        Task<Stream> GetStreamAsync(string requestUri, Dictionary<string, string> headers);

        Task<HttpResponseMessage> GetAsync(string requestUri, Dictionary<string, string> headers);
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, Dictionary<string,string> headers);
        Task<HttpResponseMessage> DeleteAsync(string requestUri, Dictionary<string, string> headers);
        Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, Dictionary<string, string> headers);
        Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content, Dictionary<string, string> headers);
    }
}
