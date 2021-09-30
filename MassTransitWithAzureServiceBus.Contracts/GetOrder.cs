namespace MassTransitWithAzureServiceBus.Contracts
{
    public class GetOrder : IGetOrder
    {
        public int MerchantId { get; set; }
    }
}
