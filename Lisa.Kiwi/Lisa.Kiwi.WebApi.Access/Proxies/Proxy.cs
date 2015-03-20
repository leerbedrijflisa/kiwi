using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Lisa.Kiwi.WebApi
{
    public class Proxy<T>
    {
        public Proxy(string baseUrl, string resourceUrl, string token = null, string tokenType = null)
        {
            _baseUrl = baseUrl.Trim('/');
            _resourceUrl = resourceUrl.Trim('/');
            _client = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
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

            Authorize(_client, token, tokenType);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            var result = await _client.GetAsync(_resourceUrl);

            var json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json, _settings);
        }

        public async Task<T> GetAsync(int id)
        {
            var url = String.Format("{0}/{1}", _resourceUrl, id);
            var result = await _client.GetAsync(url);

            var json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public async Task<T> PostAsync(T model)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(string.Format("{0}/{1}", _baseUrl, _resourceUrl)),
                Content = new StringContent(JsonConvert.SerializeObject(model, _settings), Encoding.UTF8, "Application/json")
            };

            var result = await _client.SendAsync(request);
            var json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public async Task<T> PatchAsync(int id, T model)
        {
            var request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(String.Format("{0}/{1}/{2}", _baseUrl, _resourceUrl, id)),
                Content = new StringContent(JsonConvert.SerializeObject(model, _settings), Encoding.UTF8, "application/json")
            };

            var result = await _client.SendAsync(request);
            var json = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        private void Authorize(HttpClient client, string token, string tokenType)
        {
            if (token != null && tokenType != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", String.Format("{0} {1}", tokenType, token));
            }
        }

        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly string _resourceUrl;
        private readonly JsonSerializerSettings _settings;
    }
}