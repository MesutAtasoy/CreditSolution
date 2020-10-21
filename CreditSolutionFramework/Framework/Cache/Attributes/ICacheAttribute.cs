﻿using System.Reflection;
using System.Threading.Tasks;

namespace Framework.Cache.Attributes
{
    public interface ICacheAttribute
    {
        Task<string> OnBeforeAsync(MethodInfo targetMethod, object[] args, ICustomDistributedCache distributedCache);
        string OnBefore(MethodInfo targetMethod, object[] args, ICustomDistributedCache distributedCache);
        Task  OnAfterAsync(MethodInfo targetMethod,  object[] args, object value,  ICustomDistributedCache distributedCache);
        void  OnAfter(MethodInfo targetMethod,  object[] args, object value,  ICustomDistributedCache distributedCache);
    }
}