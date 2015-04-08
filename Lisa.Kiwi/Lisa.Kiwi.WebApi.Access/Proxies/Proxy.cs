using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using Newtonsoft.Json.Converters;

namespace Lisa.Kiwi.WebApi
{
    public class Proxy<T>
    {
        public Proxy(string baseUrl, string resourceUrl)
        {
            _apiBaseUrl = baseUrl.Trim('/');
            _proxyResourceUrl = resourceUrl.Trim('/');

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_apiBaseUrl)
            };

            _jsonSerializerSettings = new JsonSerializerSettings
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

        public Proxy(string baseUrl, string resourceUrl, string tokenType, string token)
            : this(baseUrl, resourceUrl)
        {
            if (token != null && tokenType != null)
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("{0} {1}", tokenType, token));
            }
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            var result = await _httpClient.GetAsync(_proxyResourceUrl);
            return await DeserializeList(result);
        }

        public async Task<T> GetAsync(int id)
        {
            var url = String.Format("{0}/{1}", _proxyResourceUrl, id);

            var result = await _httpClient.GetAsync(url);
            return await DeserializeSingle(result);
        }

        public async Task<T> PostAsync(T model)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(string.Format("{0}/{1}", _apiBaseUrl, _proxyResourceUrl)),
                Content = new StringContent(JsonConvert.SerializeObject(model, _jsonSerializerSettings), Encoding.UTF8, "Application/json")
            };

            var result = await _httpClient.SendAsync(request);
            return await DeserializeSingle(result);
        }

        public async Task<T> PatchAsync(int id, T model)
        {
            var request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(String.Format("{0}/{1}/{2}", _apiBaseUrl, _proxyResourceUrl, id)),
                Content = new StringContent(JsonConvert.SerializeObject(model, _jsonSerializerSettings), Encoding.UTF8, "application/json")
            };

            var result = await _httpClient.SendAsync(request);
            return await DeserializeSingle(result);
        }

        public async Task<T> DeleteAsync(int id, T model)
        {
            var request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(String.Format("{0}/{1}/{2}", _apiBaseUrl, _proxyResourceUrl, id)),
                Content = new StringContent(JsonConvert.SerializeObject(model, _jsonSerializerSettings), Encoding.UTF8, "application/json")
            };

            var result = await _httpClient.SendAsync(request);
            return await DeserializeSingle(result);
        }

        private async Task<T> DeserializeSingle(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);
        }

        private async Task<IEnumerable<T>> DeserializeList(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<T>>(json, _jsonSerializerSettings);
        }



        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly string _proxyResourceUrl;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
    }
}