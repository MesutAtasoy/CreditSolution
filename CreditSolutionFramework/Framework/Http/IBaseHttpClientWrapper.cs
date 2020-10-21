using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Framework.Shared.Models.Base;

namespace Framework.Http
{
    public interface IBaseHttpClientWrapper
    {
        Task<BaseResponseModel<T>> PostAsync<T>(string urlPath, object @object,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class;

        Task<BaseResponseModel<T>> PutAsync<T>(string urlPath, object @object,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class;

        Task<BaseResponseModel<T>> DeleteAsync<T>(string urlPath, object @object,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class;

        Task<BaseResponseModel<T>> GetAsync<T>(string urlPath, object @params,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class;

        Task<BaseResponseModel<T>> GetAsync<T>(string urlPath,
            Dictionary<string, string> requestHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) where T : class;
    }
}