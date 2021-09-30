﻿namespace MassTransitWithAzureServiceBus.Api.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId {  get; set; }
        public Order Order { get; set; }
    }
}
