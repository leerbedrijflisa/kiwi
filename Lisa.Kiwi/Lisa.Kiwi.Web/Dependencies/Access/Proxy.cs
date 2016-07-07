using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Lisa.Common.Access
{
    public class Proxy<T> where T : class
    {
        public Proxy(string resourceUrl, JsonSerializerSettings jsonSerializerSettings)
        {
            _proxyResourceUrl = new Uri(resourceUrl.Trim('/'));
            _httpClient = new HttpClient();

            _jsonSerializerSettings = jsonSerializerSettings ?? new JsonSerializerSettings
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

        public Proxy(string resourceUrl)
            : this(resourceUrl, null)
        {
        }

        public Token Token { get; set; }

        public async Task<IEnumerable<T>> GetAsync(Uri uri = null, List<Uri> redirectUriList = null)
        {
            CheckRedirectLoop(uri, ref redirectUriList);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri ?? _proxyResourceUrl
            };

            AddAuthorizationHeader(ref request);

            var result = await _httpClient.SendAsync(request);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await DeserializeList(result);

                case HttpStatusCode.TemporaryRedirect:
                case HttpStatusCode.Redirect:
                case HttpStatusCode.RedirectMethod:
                    if (result.Headers.Location != null)
                    {
                        redirectUriList.Add(result.Headers.Location);
                        return await GetAsync(result.Headers.Location, redirectUriList);
                    }
                    throw new WebApiException("Redirect without Location provided", result.StatusCode);

                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    throw new UnauthorizedAccessException();

                case HttpStatusCode.NotFound:
                case HttpStatusCode.Gone:
                    return null;
            }

            throw new WebApiException("Unexpected statuscode", result.StatusCode);
        }

        public async Task<T> GetAsync(int id, Uri uri = null, List<Uri> redirectUriList = null)
        {
            CheckRedirectLoop(uri, ref redirectUriList);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri ?? new Uri(string.Format("{0}/{1}", _proxyResourceUrl, id))
            };

            AddAuthorizationHeader(ref request);

            var result = await _httpClient.SendAsync(request);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await DeserializeSingle(result);

                case HttpStatusCode.TemporaryRedirect:
                case HttpStatusCode.Redirect:
                case HttpStatusCode.RedirectMethod:
                    if (result.Headers.Location != null)
                    {
                        redirectUriList.Add(result.Headers.Location);
                        return await GetAsync(id, result.Headers.Location, redirectUriList);
                    }
                    throw new WebApiException("Redirect without Location provided", result.StatusCode);

                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    throw new UnauthorizedAccessException();

                case HttpStatusCode.NotFound:
                case HttpStatusCode.Gone:
                    return null;
            }

            throw new WebApiException("Unexpected statuscode", result.StatusCode);
        }

        public async Task<T> PostAsync(T model, Uri uri = null, List<Uri> redirectUriList = null)
        {
            CheckRedirectLoop(uri, ref redirectUriList);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = _proxyResourceUrl,
                Content = new StringContent(JsonConvert.SerializeObject(model, _jsonSerializerSettings), Encoding.UTF8, "Application/json")
            };

            AddAuthorizationHeader(ref request);

            var result = await _httpClient.SendAsync(request);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.BadRequest:
                    return await DeserializeSingle(result);

                case HttpStatusCode.NoContent:
                    return null;

                case HttpStatusCode.TemporaryRedirect:
                case HttpStatusCode.Redirect:
                case HttpStatusCode.RedirectMethod:
                    if (result.Headers.Location != null)
                    {
                        redirectUriList.Add(result.Headers.Location);
                        return await PostAsync(model, result.Headers.Location, redirectUriList);
                    }
                    throw new WebApiException("Redirect without Location provided", result.StatusCode);

                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    throw new UnauthorizedAccessException();
            }

            throw new WebApiException("Unexpected statuscode", result.StatusCode);
        }

        public async Task<T> PatchAsync(int id, T model, Uri uri = null, List<Uri> redirectUriList = null)
        {
            CheckRedirectLoop(uri, ref redirectUriList);

            var request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(String.Format("{0}/{1}", _proxyResourceUrl, id)),
                Content = new StringContent(JsonConvert.SerializeObject(model, _jsonSerializerSettings), Encoding.UTF8, "Application/json")
            };

            AddAuthorizationHeader(ref request);

            var result = await _httpClient.SendAsync(request);

            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.NoContent:
                case HttpStatusCode.BadRequest:
                    return await DeserializeSingle(result);

                case HttpStatusCode.TemporaryRedirect:
                case HttpStatusCode.Redirect:
                case HttpStatusCode.RedirectMethod:
                    if (result.Headers.Location != null)
                    {
                        redirectUriList.Add(result.Headers.Location);
                        return await PostAsync(model, result.Headers.Location, redirectUriList);
                    }
                    throw new WebApiException("Redirect without Location provided", result.StatusCode);

                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    throw new UnauthorizedAccessException();
            }

            throw new WebApiException("Unexpected statuscode", result.StatusCode);
        }

        public async Task DeleteAsync(int id, Uri uri = null, List<Uri> redirectUriList = null)
        {
            CheckRedirectLoop(uri, ref redirectUriList);

            var request = new HttpRequestMessage
            {
                Method = new HttpMethod("DELETE"),
                RequestUri = new Uri(String.Format("{0}/{1}", _proxyResourceUrl, id))
            };

            AddAuthorizationHeader(ref request);

            var result = await _httpClient.SendAsync(request);

            switch (result.StatusCode)
            {
                case HttpStatusCode.Accepted:
                case HttpStatusCode.NoContent:
                    return;

                case HttpStatusCode.TemporaryRedirect:
                case HttpStatusCode.Redirect:
                case HttpStatusCode.RedirectMethod:
                    if (result.Headers.Location != null)
                    {
                        redirectUriList.Add(result.Headers.Location);
                        await DeleteAsync(id, result.Headers.Location, redirectUriList);
                        return;
                    }
                    throw new WebApiException("Redirect without Location provided", result.StatusCode);

                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    throw new UnauthorizedAccessException();
            }

            throw new WebApiException("Unexpected statuscode", result.StatusCode);
        }

        private void CheckRedirectLoop(Uri uri, ref List<Uri> redirectUriList)
        {
            if (redirectUriList != null && redirectUriList.Contains(uri))
            {
                throw new WebApiException("Endless redirect loop", HttpStatusCode.Redirect);
            }

            redirectUriList = new List<Uri>();
        }

        private void AddAuthorizationHeader(ref HttpRequestMessage request)
        {
            if (Token != null && !string.IsNullOrEmpty(Token.Value))
            {
                request.Headers.Add("Authorization", String.Format("{0} {1}", Token.Type, Token.Value));
            }
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
        private readonly Uri _proxyResourceUrl;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
    }


}