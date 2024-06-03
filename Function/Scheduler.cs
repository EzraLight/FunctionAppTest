using FunctionAppTest.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppTest.Function
{
    public class Scheduler
    {
        private readonly ILogger<Scheduler> _logger;
        private readonly AppDbContext _appDbContext;
        public Scheduler(ILogger<Scheduler> log, AppDbContext appDbContext)
        {
            _logger = log;
            _appDbContext = appDbContext;
        }

        [FunctionName("Scheduler_Every_2Mins")]
        public async Task Run([TimerTrigger("%LogicSchedule%")] TimerInfo myTimer, ILogger log)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            DateTime startedTime = DateTime.Now;
            Loggings rs = new()
            {
                StartedTime = startedTime,
                FunctionName = "Scheduler_Every_2Mins"
            };
            // check living time
            //await Task.Delay(TimeSpan.FromMinutes(10));
            DateTime finishTime = DateTime.Now;
            rs.FinishedTime = finishTime;
            rs.ExcutionTime = (finishTime - startedTime).TotalMinutes;
            rs.IsCompleted = true;

            await _appDbContext.Loggings.AddAsync(rs);
            await _appDbContext.SaveChangesAsync();

        }
    }
}
