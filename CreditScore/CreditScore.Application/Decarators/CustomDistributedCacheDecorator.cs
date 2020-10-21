using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Framework.Cache;
using Framework.Cache.Attributes;
using Framework.DispatchProxy.BaseDispatchProxyAsync;
using Newtonsoft.Json;

namespace CreditScore.Application.Decarators
{
    public class CustomDistributedCacheDecorator<T> : DispatchProxyAsync
    {
        private T _decorated;
        private ICustomDistributedCache _distributedCache;

        public static T Create(T decorated, ICustomDistributedCache distributedCache)
        {
            object proxy = Create<T, CustomDistributedCacheDecorator<T>>();
            ((CustomDistributedCacheDecorator<T>)proxy).SetParameters(decorated, distributedCache);
        
            return (T)proxy;
        }
        
        private void SetParameters(T decorated,ICustomDistributedCache distributedCache)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            _decorated = decorated;
            _distributedCache = distributedCache;
        }
        
        public override object Invoke(MethodInfo method, object[] args)
        {
            var aspect = method.GetCustomAttributes(typeof(ICacheAttribute), true).FirstOrDefault();

            if (aspect == null)
            {
                return method.Invoke(_decorated, args);
            }
            
            var beforeResponse =  ((ICacheAttribute) aspect)?.OnBefore(method, args, _distributedCache);

            object result;
            if (string.IsNullOrEmpty(beforeResponse))
            {
                result = method.Invoke(_decorated, args);
                (aspect as ICacheAttribute)?.OnAfter(method, args, result, _distributedCache);
            }
            else
            {
                result = JsonConvert.DeserializeObject<object>(beforeResponse);
            }
            
            return result;        
        }

        public override async Task InvokeAsync(MethodInfo method, object[] args)
        {
            var aspect = method.GetCustomAttributes(typeof(ICacheAttribute), true).FirstOrDefault();

            if (aspect == null)
            {
                var methodTaskResult = method.Invoke(_decorated, args);
                await ((Task) methodTaskResult);
                return;
            }
            
            var beforeResponse = await ((ICacheAttribute) aspect)?.OnBeforeAsync(method, args, _distributedCache);

            if (string.IsNullOrEmpty(beforeResponse))
            {
                var methodResult = method.Invoke(_decorated, args);
                await ((Task) methodResult);
            }
        }

        public override async Task<T1> InvokeAsyncT<T1>(MethodInfo method, object[] args)
        {
            try
            {
                var aspect = method.GetCustomAttributes(typeof(ICacheAttribute), true).FirstOrDefault();

                if (aspect == null)
                {
                    var methodTaskResult = method.Invoke(_decorated, args);
                    return await (Task<T1>) methodTaskResult;
                }
            
                var beforeResponse = await ((ICacheAttribute) aspect)?.OnBeforeAsync(method, args, _distributedCache);

                T1 response;
                if (string.IsNullOrEmpty(beforeResponse))
                {
                    var methodResult = method.Invoke(_decorated, args);
                    response =  await ((Task<T1>) methodResult);
                    await (aspect as ICacheAttribute)?.OnAfterAsync(method, args, response, _distributedCache);
                }
                else
                {
                    response = JsonConvert.DeserializeObject<T1>(beforeResponse);
                }
            
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}