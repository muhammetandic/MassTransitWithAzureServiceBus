using MassTransit;
using MassTransitWithAzureServiceBus.Api.ApplicationDbContext;
using MassTransitWithAzureServiceBus.Api.Models;
using MassTransitWithAzureServiceBus.Contracts;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace MassTransitWithAzureServiceBus.Api.Consumer
{
    public class OrderReturnedConsumer : IConsumer<OrderReturned>
    {
        private readonly ILogger<OrderReturnedConsumer> _logger;
        private readonly AppDbContext _dbContext;

        public OrderReturnedConsumer(ILogger<OrderReturnedConsumer> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public Task Consume(ConsumeContext<OrderReturned> context)
        {
            var message = context.Message;
            var order = new Order
            {
                CustomerId = message.CustomerId,
                CustomerName = message.CustomerName,
                CustomerPhone = message.CustomerPhone,
                Products = message.Products.Select(p => new Models.Product { OrderId = message.OrderId, ProductName = p.ProductName, Quantity = p.Quantity, Price = p.Price }).ToList()
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            _logger.LogInformation(context.Message.ToString());
            return Task.CompletedTask;
        }
    }
}
