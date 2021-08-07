using Application.DAL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface ICartService
    {
        Task AddOfferToCartAsync(long offerId, long userId);
        Task<IEnumerable<CartOfferDTO>> GetOffersFromCartAsync(long cartId);
        Task RemoveOfferFromCartAsync(long relationId);
        Task<long> Create(long userId);
        Task UpdateProductCountAsync(long userId, long offerId, int productCount);
    }
}
