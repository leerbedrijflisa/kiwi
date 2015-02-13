using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lisa.Kiwi.WebApi
{
    public class Proxy<T>
    {
        public Proxy(string baseUrl, string resourceUrl)
        {
            _baseUrl = baseUrl;
            _resourceUrl = resourceUrl;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var result = await client.GetAsync(_resourceUrl);

                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            }
        }

        public async Task<T> GetAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var url = String.Format("{0}/{1}", _resourceUrl, id);
                var result = await client.GetAsync(url);

                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        private string _baseUrl;
        private string _resourceUrl;
    }
}