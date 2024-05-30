using FunctionAppTest.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace FunctionAppTest
{
    public class RedisCacheAccessorFunc
    {
        private readonly IRedisCacheAccessor _redisCacheAccessor;

        public RedisCacheAccessorFunc(IRedisCacheAccessor redisCacheAccessor)
        {
            _redisCacheAccessor = redisCacheAccessor;
        }

        /// <summary>
        /// PUT request will add to the redis cache.  GET request will retrieve from the redis cache.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("GetPutRedisCache")]
        [OpenApiOperation(operationId: "GetPutRedisCache", tags: new[] { "GetPutRedisCache" })]

        public IActionResult GetPutRedisCache(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "put", Route = null)] HttpRequest req, ILogger log)
        {

            var reqHeaders = req.Headers;
            string cacheKey = reqHeaders["cacheKey"];
            string cacheValue = reqHeaders["cacheValue"];
            string cacheConnectionString = Environment.GetEnvironmentVariable("redisCacheConnectionString");

            try
            {
                string retValue = _redisCacheAccessor.ReadWriteFromCache(cacheConnectionString, req.Method, cacheKey, cacheValue);
                return new OkObjectResult(retValue);
            }
            catch (Exception e)
            {
                log.LogError(e.Message, e);
                throw;
            }
        }
    }
}
