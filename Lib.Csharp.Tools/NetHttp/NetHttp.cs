using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools.NetHttp
{
    /// <summary>
    /// 继承于HttpClient
    /// 自动实现定义的INetHttp接口
    /// </summary>
    public class NetHttp : INetHttp
    {
        public TimeSpan Timeout { get; set; }
        

        private void SetHeader(HttpClient client, Dictionary<string,string> headers)
        {
            if (Timeout != default(TimeSpan))
                client.Timeout = Timeout;
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }
        }

        public async Task<string> GetStringAsync(string requestUri, Dictionary<string, string> headers)
        {
            using (var client = new HttpClient())
            {
                SetHeader(client, headers);
                return await client.GetStringAsync(requestUri);
            }
        }
        public async Task<Stream> GetStreamAsync(string requestUri, Dictionary<string, string> headers)
        {
            using (var client = new HttpClient())
            {
                SetHeader(client, headers);
                return await client.GetStreamAsync(requestUri);
            }
        }


        public async Task<HttpResponseMessage> GetAsync(string requestUri, Dictionary<string, string> headers)
        {
            using (var client = new HttpClient())
            {
                SetHeader(client, headers);
                return await client.GetAsync(requestUri);
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content,
            Dictionary<string, string> headers)
        {
            using (var client = new HttpClient())
            {
                SetHeader(client, headers);
                return await client.PostAsync(requestUri, content);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri, Dictionary<string, string> headers)
        {
            using (var client = new HttpClient())
            {
                SetHeader(client, headers);
                return await client.DeleteAsync(requestUri);
            }
        }

        public async Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content,
            Dictionary<string, string> headers)
        {
            using (var client = new HttpClient())
            {
                SetHeader(client, headers);
                return await client.PutAsync(requestUri, content);
            }
        }

        public async Task<HttpResponseMessage> PatchAsync(string requestUri, HttpContent content,
            Dictionary<string, string> headers)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
            {
                Content = content
            };

            using (var client = new HttpClient())
            {
                SetHeader(client, headers);
                return await client.SendAsync(request);
            }
        }

        /*public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            using (var client = new HttpClient())
            {
                return client.SendAsync(request);
            }
        }*/
    }
}
