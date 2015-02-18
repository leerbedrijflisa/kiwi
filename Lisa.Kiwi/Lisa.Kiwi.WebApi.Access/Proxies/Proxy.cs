using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Text;

namespace Lisa.Kiwi.WebApi
{
    public class Proxy<T>
    {
        public Proxy(string baseUrl, string resourceUrl)
        {
            _baseUrl = baseUrl.Trim('/');
            _resourceUrl = resourceUrl.Trim('/');
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>
                {
                    new StringEnumConverter
                    {
                        CamelCaseText = true
                    }
                }
            };
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                var result = await client.GetAsync(_resourceUrl);

                var json = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(json, _settings);
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
                return JsonConvert.DeserializeObject<T>(json, _settings);
            }
        }

        public async Task<T> PostAsync(T model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var result = await client.PostAsJsonAsync(_resourceUrl, model);
                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json, _settings);
            }
        }

        public async Task<T> PatchAsync(int id, T model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var request = new HttpRequestMessage
                {
                    Method = new HttpMethod("PATCH"),
                    RequestUri = new Uri(String.Format("{0}/{1}/{2}", _baseUrl, _resourceUrl, id)),
                    Content = new StringContent(JsonConvert.SerializeObject(model, _settings), Encoding.UTF8, "application/json")
                };

                var result = await client.SendAsync(request);
                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json, _settings);
            }
        }

        private string _baseUrl;
        private string _resourceUrl;
        private JsonSerializerSettings _settings;
    }
}