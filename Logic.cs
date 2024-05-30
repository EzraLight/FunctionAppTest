using FunctionAppTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppTest
{
    public class Logic
    {
        private readonly ILogger<Logic> _logger;
        private readonly AppDbContext _appDbContext;
        public Logic(ILogger<Logic> log, AppDbContext appDbContext)
        {
            _logger = log;
            _appDbContext = appDbContext;
        }

        [FunctionName("RunLogic")]
        [OpenApiOperation(operationId: "RunLogic", tags: new[] { "logic" })]     

        public async Task<IActionResult> RunLogic(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            DateTime startedTime = DateTime.Now;    
            Loggings rs = new() 
            { 
                StartedTime = startedTime,
                
            };
            await Task.Delay(TimeSpan.FromMinutes(10));
            DateTime finishTime = DateTime.Now;
            rs.FinishedTime = finishTime;
            rs.ExcutionTime = (startedTime - finishTime).TotalMinutes;
            rs.IsCompleted = true;

            await _appDbContext.Loggings.AddAsync(rs);
            await _appDbContext.SaveChangesAsync();
            return new OkObjectResult(rs);
        }


    }
}
