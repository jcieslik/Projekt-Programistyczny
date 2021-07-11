using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Add;
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
        Task DecrementOfferCountInCart(long userId, long offerId);
    }
}
