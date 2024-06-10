using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PatientService.Domain.Repositories;
using PatientService.Data.Context;

namespace PatientService.Domain.Services
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OutboxProcessor> _logger;

        public OutboxProcessor(IServiceProvider serviceProvider, ILogger<OutboxProcessor> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OutboxProcessor started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<PatientDbContext>();
                    var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                    var messages = dbContext.OutboxMessages.ToList();

                    foreach (var message in messages)
                    {
                        try
                        {
                            var endpoint = new Uri(message.Destination);
                            await publishEndpoint.Publish(endpoint, message.Content, stoppingToken);
                            dbContext.OutboxMessages.Remove(message);
                            await dbContext.SaveChangesAsync(stoppingToken);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error processing outbox message.");
                        }
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

            _logger.LogInformation("OutboxProcessor stopped.");
        }
    }
}
