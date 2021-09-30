using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitWithAzureServiceBus.Contracts
{
    public interface IGetOrder
    {
        public int MerchantId { get; set; }
    }
}
