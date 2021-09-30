using System.Collections.Generic;

namespace MassTransitWithAzureServiceBus.Api.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId {  get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public ICollection<Product> Products {  get; set; }   

    }
}
