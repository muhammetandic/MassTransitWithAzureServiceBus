using MassTransit;
using MassTransitWithAzureServiceBus.Contracts;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace MassTransitWithAzureServiceBus.OrdersWorker
{
    internal class Worker : BackgroundService
    {
        private readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new GetOrder { MerchantId = 1 });
                await Task.Delay(5000);
            }
        }
    }
}
