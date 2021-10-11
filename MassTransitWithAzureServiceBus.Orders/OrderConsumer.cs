using MassTransit;
using MassTransitWithAzureServiceBus.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MassTransitWithAzureServiceBus.Orders
{
    public class OrderConsumer : IConsumer<GetOrder>
    {
        private readonly ILogger<OrderConsumer> _logger;
        private readonly IBus _bus;

        public OrderConsumer(ILogger<OrderConsumer> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public Task Consume(ConsumeContext<GetOrder> context)
        {
            _logger.LogInformation(context.Message.ToString());

            var Orders = new OrderReturned
            {
                CustomerId = "1",
                CustomerName = "Muhammet Andiç",
                CustomerPhone = "4364",
                MerchantId = 1,
                OrderId = 2,
                Products = new List<Contracts.Product>()
                {
                    new Contracts.Product { Price=2.5M, ProductId=4, ProductName="Bisküvi", Quantity=4},
                    new Contracts.Product{ Price=1.45M, Quantity=2, ProductName="Çikolata", ProductId=3}
                }
            };

            _bus.Publish(Orders);
            return Task.CompletedTask;
        }
    }
}