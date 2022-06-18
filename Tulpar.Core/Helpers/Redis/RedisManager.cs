using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using Tulpar.Core.Const;
using Tulpar.Core.ModelsBase;

namespace Tulpar.Core.Helpers.Redis
{
    public interface ICacheProvider
    {
        ApiResult<T> Add<T>(string cacheKey, T model);
        ApiResult<T> Add<T>(string cacheKey, T model, TimeSpan timeout);
        ApiResult<T> Get<T>(string cacheKey);
        ApiResult Delete(string cacheKey);
        ApiResult<List<T>> GetList<T>(string cacheKey);
        ApiResult<bool> IsInCache(string key);
    }
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly RedisEndpoint _endPoint;

        public RedisCacheProvider()
        {
            _endPoint = new RedisEndpoint(ProjectSettings.Product_RedisCache_Host, ProjectSettings.Product_RedisCache_Port, ProjectSettings.Product_RedisCache_Password, ProjectSettings.Product_RedisCache_DatabaseId);
        }

        public ApiResult<T> Add<T>(string cacheKey, T model)
        {
            try
            {
                using (IRedisClient client = new RedisClient(_endPoint))
                {
                    client.Set<T>(cacheKey, model, TimeSpan.Zero);
                    return new ApiResult<T> { IsSuccess = true, Data = model };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<T> { IsSuccess = false, ErrorMessages = { ex.ToString() } };
            }
        }

        public ApiResult<T> Add<T>(string cacheKey, T model, TimeSpan timeout)
        {
            try
            {
                using (IRedisClient client = new RedisClient(_endPoint))
                {
                    client.As<T>().SetValue(cacheKey, model, timeout);
                    return new ApiResult<T> { IsSuccess = true, Data = model };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<T> { IsSuccess = false, ErrorMessages = { ex.ToString() } };
            }
        }

        public ApiResult<T> Get<T>(string cacheKey)
        {
            try
            {
                using (IRedisClient client = new RedisClient(_endPoint))
                {

                    IRedisTypedClient<T> wrapper = client.As<T>();
                    return new ApiResult<T> { IsSuccess = true, Data = wrapper.GetValue(cacheKey) };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult<T> { IsSuccess = false, ErrorMessages = { ex.ToString() } };
            }
        }

        public ApiResult Delete(string cacheKey)
        {
            try
            {
                using (IRedisClient client = new RedisClient(_endPoint))
                {
                    bool result = client.Remove(cacheKey);
                    return new ApiResult { IsSuccess = result };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult { IsSuccess = false, ErrorMessages = { ex.ToString() } };
            }
        }

        public ApiResult<List<T>> GetList<T>(string cacheKey)
        {
            try
            {
                List<T> result = new List<T>();
                using (RedisClient client = new RedisClient(_endPoint))
                {
                    List<string> allKeys = client.SearchKeys(cacheKey + "*");
                    foreach (string item in allKeys)
                    {
                        result.Add(client.Get<T>(item));
                    }
                }
                return new ApiResult<List<T>> { IsSuccess = true, Data = result };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<T>> { IsSuccess = false, ErrorMessages = { ex.ToString() } };
            }
        }

        public ApiResult<bool> IsInCache(string key)
        {
            try
            {
                bool isInCache = false;

                using (RedisClient client = new RedisClient(_endPoint))
                {
                    isInCache = client.ContainsKey(key);
                }
                return new ApiResult<bool> { IsSuccess = isInCache, Data = isInCache };
            }
            catch (Exception ex)
            {
                return new ApiResult<bool> { ErrorMessages = { ex.ToString() } };
            }
        }

    }
}
