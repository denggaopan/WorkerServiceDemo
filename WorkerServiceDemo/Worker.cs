using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WorkerServiceDemo.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WorkerServiceDemo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private DbContext _db;
        //private IServiceProvider _sp;

        public Worker(ILogger<Worker> logger
            //,DbContext db
           // ,IServiceProvider sp
            )
        {
            _logger = logger;
            //_db = db;
            //_sp = sp;
        }
        

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _db = new ApplicationDbContext();
            // _db = _sp.GetRequiredService<DbContext>();
            _logger.LogInformation("Worker starting at: {time}", DateTimeOffset.Now);
            _db.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"Worker starting", CreatedTime = DateTime.Now });
            _db.SaveChanges();
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    _db.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"Worker running", CreatedTime = DateTime.Now });
                    _db.SaveChanges();
                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Worker running exception at: {time}", DateTimeOffset.Now);
                _db.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"Worker running exception : {ex.Message}", CreatedTime = DateTime.Now });
                _db.SaveChanges();
            }
            finally
            {
                _logger.LogInformation("Worker running finally at: {time}", DateTimeOffset.Now);
                _db.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"Worker running finally", CreatedTime = DateTime.Now });
                _db.SaveChanges();

                _db.Dispose();
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker stoping at: {time}", DateTimeOffset.Now);
            _db.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"Worker stoping", CreatedTime = DateTime.Now });
            _db.SaveChanges();
            await base.StopAsync(cancellationToken);
        }




    }
}
