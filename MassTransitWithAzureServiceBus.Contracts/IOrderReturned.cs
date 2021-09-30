using System.Collections.Generic;

namespace MassTransitWithAzureServiceBus.Contracts
{
    public interface IOrderReturned
    {
        public int MerchantId { get; set; }
        public int OrderId {  get; set; }
        public string CustomerId {  get; set; }
        public string CustomerName {  get; set; }
        public string CustomerPhone {  get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity {  get; set; }
        public decimal Price {  get; set; }
    }
}
