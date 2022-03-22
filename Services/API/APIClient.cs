using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services.API
{
    public class APIClient
    {
        private readonly HttpClient httpClient;
        public APIClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<APIResponse> Post(string url, string token, string parameters)
        {
            StringContent stringContent = InicializarResponse(token, parameters);
            using HttpResponseMessage response = await httpClient.PostAsync(url, stringContent);
            string sResponse = await response.Content.ReadAsStringAsync();
            return new APIResponse(response.StatusCode, sResponse);
        }

        public async Task<APIResponse> Put(string url, string token, string parameters)
        {
            try
            {
                StringContent stringContent = InicializarResponse(token, parameters);
                using HttpResponseMessage response = await httpClient.PutAsync(url, stringContent);
                string sResponse = await response.Content.ReadAsStringAsync();
                return new APIResponse(response.StatusCode, sResponse);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<APIResponse> Patch(string url, string token, string parameters)
        {
            try
            {
                StringContent stringContent = InicializarResponse(token, parameters);
                using HttpResponseMessage response = await httpClient.PatchAsync(url, stringContent);
                string sResponse = await response.Content.ReadAsStringAsync();
                return new APIResponse(response.StatusCode, sResponse);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<APIResponse> PatchFormData(string url, string token, IFormFileCollection files, IFormCollection keyValues)
        {
            try
            {
                HttpContent content = BuildFormData(token, files, keyValues);
                using HttpResponseMessage response = await httpClient.PatchAsync(url, content);
                string sResponse = await response.Content.ReadAsStringAsync();
                return new APIResponse(response.StatusCode, sResponse);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<APIResponse> PostFormData(string url, string token, Dictionary<string, IFormFile> files)
        {
            try
            {
                MultipartFormDataContent content = InicializarResponseFormData(token, files);
                using HttpResponseMessage response = await httpClient.PostAsync(url, content);
                string sResponse = await response.Content.ReadAsStringAsync();
                return new APIResponse(response.StatusCode, sResponse);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<APIResponse> PostFormDataWithOtherValues(string url, string token, IFormFileCollection files, IFormCollection values)
        {
            try
            {
                HttpContent content = BuildFormData(token, files, values);
                using HttpResponseMessage response = await httpClient.PostAsync(url, content);
                string sResponse = await response.Content.ReadAsStringAsync();
                return new APIResponse(response.StatusCode, sResponse);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<APIResponse> Get(string url, string token)
        {
            try
            {
                InicializarResponseGet(token);
                using HttpResponseMessage response = await httpClient.GetAsync(url);
                string sResponse = await response.Content.ReadAsStringAsync();
                return new APIResponse(response.StatusCode, sResponse);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<APIResponse> GetQuery(string url, string token, string parameters)
        {
            try
            {
                url = $"{url}{InicializarResponseGetQuery(token, parameters)}";
                using HttpResponseMessage response = await httpClient.GetAsync(url);
                string sResponse = await response.Content.ReadAsStringAsync();
                return new APIResponse(response.StatusCode, sResponse);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private string InicializarResponseGetQuery(string token, string parameters)
        {
            try
            {
                var jObject = (JObject)JsonConvert.DeserializeObject(parameters);
                var dic = new Dictionary<string, string>();
                var uri = "?";
                var keyValue = jObject.GetEnumerator();

                bool isTrue = !string.IsNullOrWhiteSpace(parameters);

                while (isTrue)
                {
                    if (keyValue.Current.Key != null)
                    {
                        if (!string.IsNullOrWhiteSpace(keyValue.Current.Value.ToString()))
                        {
                            dic.Add(keyValue.Current.Key, keyValue.Current.Value.ToString());
                        }
                    }
                    isTrue = keyValue.MoveNext();
                }

                foreach (var item in dic)
                {
                    if (item.Value.StartsWith("["))
                    {
                        var strings = item.Value.Replace("[", "").Replace("]", "").Replace("\n  \"", "").Replace("\"", "").Replace("\n", "").Split(",");

                        foreach (var s in strings)
                        {
                            uri += $"{item.Key}={s}&";
                        }
                    }
                    else
                    {
                        uri += $"{item.Key}={item.Value}&";
                    }
                }
                if (uri != "?")
                    uri = uri.Remove(uri.LastIndexOf("&"));

                httpClient.DefaultRequestHeaders.Clear();
                if (token != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                return uri;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private StringContent InicializarResponse(string token, string parameters)
        {
            StringContent stringContent = parameters != null ? new StringContent(parameters, Encoding.UTF8, "application/json") : null;
            httpClient.DefaultRequestHeaders.Clear();
            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return stringContent;
        }

        private void InicializarResponseGet(string token)
        {
            httpClient.DefaultRequestHeaders.Clear();
            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private MultipartFormDataContent InicializarResponseFormData(string token, Dictionary<string, IFormFile> files)
        {
            var multipartContent = new MultipartFormDataContent();

            foreach (var file in files)
            {
                multipartContent.Add(new StreamContent(file.Value.OpenReadStream()), file.Key, file.Value.FileName);
            }

            httpClient.DefaultRequestHeaders.Clear();
            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return multipartContent;
        }

        private HttpContent BuildFormData(string token, IFormFileCollection files, IFormCollection values)
        {
            httpClient.DefaultRequestHeaders.Clear();
            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var multipartContent = new MultipartFormDataContent();
            multipartContent.Headers.ContentType.MediaType = "multipart/form-data";

            if (files != null)
            {
                foreach (var file in files)
                {
                    multipartContent.Add(new StreamContent(file.OpenReadStream()), file.Name, file.FileName);
                }
            }

            if (values != null)
            {
                foreach (var value in values)
                {
                    multipartContent.Add(new StringContent(value.Value), value.Key);
                }
            }

            return multipartContent;
        }
    }
}
