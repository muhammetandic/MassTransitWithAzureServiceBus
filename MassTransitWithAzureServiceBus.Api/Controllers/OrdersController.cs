using MassTransit;
using MassTransitWithAzureServiceBus.Api.ApplicationDbContext;
using MassTransitWithAzureServiceBus.Api.Models;
using MassTransitWithAzureServiceBus.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace MassTransitWithAzureServiceBus.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IBus _bus;
        private readonly AppDbContext _dbContext;

        public OrdersController(ILogger<OrdersController> logger, IBus bus, AppDbContext dbContext)
        {
            _logger = logger;
            _bus = bus;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Order> GetAsync()
        {
            var orders = _dbContext.Orders.Include(c=>c.Products).ToList();
            return orders;
        }

        [HttpGet]
        [Route("Bring")]
        public IActionResult Bring()
        {
            _bus.Publish(new GetOrder { MerchantId = 1 });
            return Ok();
        }
    }
}
