using System.Collections.Generic;

namespace MassTransitWithAzureServiceBus.Contracts
{
    public class OrderReturned : IOrderReturned
    {
        public int MerchantId { get; set; }
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public List<Product> Products { get; set; }
    }
}
