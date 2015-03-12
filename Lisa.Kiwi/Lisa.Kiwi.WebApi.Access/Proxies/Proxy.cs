using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Text;

namespace Lisa.Kiwi.WebApi
{
    public class Proxy<T>
    {
        public Proxy(string baseUrl, string resourceUrl, string token = null, string tokenType = null)
        {
            _baseUrl = baseUrl.Trim('/');
            _resourceUrl = resourceUrl.Trim('/');
            _token = token;
            _tokenType = tokenType;
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                //Converters = new List<JsonConverter>
                //{
                //    new StringEnumConverter
                //    {
                //        CamelCaseText = true
                //    }
                //}
            };
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            using (var client = new HttpClient())
            {
                Authorize(client);

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
                Authorize(client);

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
                Authorize(client);

                client.BaseAddress = new Uri(_baseUrl);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(string.Format("{0}/{1}", _baseUrl, _resourceUrl)),
                    Content = new StringContent(JsonConvert.SerializeObject(model, _settings), Encoding.UTF8, "Application/json")
                };

                var result = await client.SendAsync(request);
                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(json, _settings);
            }
        }

        public async Task<T> PatchAsync(int id, T model)
        {
            using (var client = new HttpClient())
            {
                Authorize(client);

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

        private void Authorize(HttpClient client)
        {
            if (_token != null && _tokenType != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", String.Format("{0} {1}", _tokenType, _token));
            }
        }

        private readonly string _baseUrl;
        private readonly string _resourceUrl;
        private readonly string _token;
        private readonly string _tokenType;
        private readonly JsonSerializerSettings _settings;
    }
}