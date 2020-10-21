using Framework.Shared.Models.Base;
using Newtonsoft.Json;

namespace Framework.Extensions
{
    public static class BaseResponseModelExtensions
    {
        public static T ConvertResultTo<T>(this BaseResponseModel model)
            => (T)JsonConvert.DeserializeObject(model.Payload.ToString(), typeof(T));
    }
}