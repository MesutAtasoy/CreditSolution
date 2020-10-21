using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Framework.Extensions;
using Framework.Shared.Models.Base;
using Framework.Shared.Models.Enums;
using Newtonsoft.Json;

namespace Framework.Http
{
    public class BaseHttpClientWrapper : IBaseHttpClientWrapper
    {
        protected readonly HttpClient _httpClient;

        public BaseHttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponseModel<T>> PostAsync<T>(string urlPath, object @object,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var json = ConvertToStringContent(ConvertToJsonBody(@object));

            try
            {
                var responseHttpMessage = await _httpClient.PostAsync(urlPath, json, cancellationToken);
                return await HandleResponseAsync<T>(responseHttpMessage);
            }
            catch (Exception ex)
            {
                return HandleException<T>(ex);
            }
        }

        public async Task<BaseResponseModel<T>> PutAsync<T>(string urlPath, object @object,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var json = ConvertToStringContent(ConvertToJsonBody(@object));

            try
            {
                var responseHttpMessage = await _httpClient.PutAsync(urlPath, json, cancellationToken);
                return await HandleResponseAsync<T>(responseHttpMessage);
            }
            catch (Exception e)
            {
                return HandleException<T>(e);
            }
        }

        public async Task<BaseResponseModel<T>> DeleteAsync<T>(string urlPath, object @object,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var json = ConvertToStringContent(ConvertToJsonBody(@object));

            try
            {
                var responseHttpMessage = await _httpClient.DeleteAsync(urlPath, cancellationToken);
                return await HandleResponseAsync<T>(responseHttpMessage);
            }
            catch (Exception e)
            {
                return HandleException<T>(e);
            }
        }

        public async Task<BaseResponseModel<T>> GetAsync<T>(string urlPath,
            object @params,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            urlPath = UrlExtensions.GetUrlWithQueryObject(urlPath, @params);

            try
            {
                var responseHttpMessage = await _httpClient.GetAsync(urlPath, cancellationToken);
                return await HandleResponseAsync<T>(responseHttpMessage);
            }
            catch (Exception e)
            {
                return HandleException<T>(e);
            }
        }

        public async Task<BaseResponseModel<T>> GetAsync<T>(string urlPath,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            try
            {
                var responseHttpMessage = await _httpClient.GetAsync(urlPath, cancellationToken);
                return await HandleResponseAsync<T>(responseHttpMessage);
            }
            catch (Exception e)
            {
                return HandleException<T>(e);
            }
        }

        protected StringContent ConvertToStringContent(string strContent)
        {
            return new StringContent(strContent, Encoding.UTF8, "application/json");
        }

        protected string ConvertToJsonBody(object @object)
        {
            return JsonConvert.SerializeObject(@object);
        }

        protected async Task<BaseResponseModel<T>> HandleResponseAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            try
            {
                var resultContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var resultObject = JsonConvert.DeserializeObject<BaseResponseModel<T>>(resultContent);
                return resultObject;
            }
            catch (Exception e)
            {
                return HandleException<T>(e);
            }
        }

        private BaseResponseModel<T> HandleException<T>(Exception e)
        {
            var response = new BaseResponseModel<T>
            {
                StatusCode = 500,
                Exception = e.InnerException,
            };
            response.AddMessage(new ResponseMessage("Bir hata oluştu", null, ResponseMessageType.BadRequest));
            Console.WriteLine(e);
            return response;
        }
    }
}