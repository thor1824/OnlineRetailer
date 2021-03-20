using Or.Domain.Model.SharedModels;
using Or.Micro.Orders.Models;
using System.Threading.Tasks;

namespace Or.Micro.Orders.MessageGateways
{
    public interface IProductMessageGateway
    {
        Task PublishStockValidationRequestAsync(Order order);
    }
}