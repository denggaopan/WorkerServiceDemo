using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkerServiceDemo.Entities;

namespace WorkerServiceDemo
{
    public class WorkerPro : BackgroundService
    {
        private readonly ILogger<WorkerPro> _logger;
        private DbContext _db;

        public WorkerPro(ILogger<WorkerPro> logger)
        {
            _logger = logger;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _db = new ApplicationDbContext();
            _logger.LogInformation("WorkerPro starting at: {time}", DateTimeOffset.Now);
            _db.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro starting", CreatedTime = DateTime.Now });
            _db.SaveChanges();
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("WorkerPro running tasks at: {time}", DateTimeOffset.Now);
                _db.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro running tasks", CreatedTime = DateTime.Now });
                _db.SaveChanges();

                var task1 = Run1(stoppingToken);
                var task2 = Run2(stoppingToken);
                var task3 = Run3(stoppingToken);

                await Task.WhenAll(task1, task2, task3);
            }
            catch (Exception)
            {
                _logger.LogInformation("WorkerPro running exception at: {time}", DateTimeOffset.Now);
                _db.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro running exception", CreatedTime = DateTime.Now });
                _db.SaveChanges();
            }
            finally
            {
                _logger.LogInformation("WorkerPro running finally at: {time}", DateTimeOffset.Now);
                _db.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro running finally", CreatedTime = DateTime.Now });
                _db.SaveChanges();

                _db.Dispose();
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("WorkerPro stoping at: {time}", DateTimeOffset.Now);
            _db.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro stoping", CreatedTime = DateTime.Now });
            _db.SaveChanges();
            await base.StopAsync(cancellationToken);
        }


        protected Task Run1(CancellationToken stoppingToken)
        {
            var task = Task.Run(() =>
            {
                var _db1 = new ApplicationDbContext();
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogInformation("WorkerPro Run1 running at: {time}", DateTimeOffset.Now);
                        _db1.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro Run1 running", CreatedTime = DateTime.Now });
                        _db1.SaveChanges();
                        Thread.Sleep(1000);
                    }
                }catch (Exception ex)
                {
                    _db1.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro Run1 running exception : {ex.Message}", CreatedTime = DateTime.Now });
                    _db1.SaveChanges();
                }
                finally
                {
                    _db1.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro Run1 running finally", CreatedTime = DateTime.Now });
                    _db1.SaveChanges();

                    _db1.Dispose();

                }
            },stoppingToken);
            return task;
        }

        protected Task Run2(CancellationToken stoppingToken)
        {
            var task = Task.Run(() =>
            {
                var _db2 = new ApplicationDbContext();
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogInformation("WorkerPro Run2 running at: {time}", DateTimeOffset.Now);
                        _db2.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro Run2 running", CreatedTime = DateTime.Now });
                        _db2.SaveChanges();
                        Thread.Sleep(2000);
                    }
                }
                catch (Exception ex)
                {
                    _db2.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro Run2 running exception : {ex.Message}", CreatedTime = DateTime.Now });
                    _db2.SaveChanges();
                }
                finally
                {
                    _db2.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro Run2 running finally", CreatedTime = DateTime.Now });
                    _db2.SaveChanges();

                    _db2.Dispose();

                }
            }, stoppingToken);
            return task;
        }

        protected Task Run3(CancellationToken stoppingToken)
        {
            var task = Task.Run(() =>
            {
                var _db3 = new ApplicationDbContext();

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogInformation("WorkerPro Run3 running at: {time}", DateTimeOffset.Now);
                        _db3.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro Run3 running", CreatedTime = DateTime.Now });
                        _db3.SaveChanges();
                        Thread.Sleep(3000);
                    }
                }
                catch (Exception ex)
                {
                    _db3.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro Run3 running exception : {ex.Message}", CreatedTime = DateTime.Now });
                    _db3.SaveChanges();
                }
                finally
                {
                    _db3.Set<SystemLog>().Add(new SystemLog { Id = Guid.NewGuid().ToString(), Level = "Info", Message = $"WorkerPro Run3 running finally", CreatedTime = DateTime.Now });
                    _db3.SaveChanges();

                    _db3.Dispose();
                }
            }, stoppingToken);
            return task;
        }

    }
}
