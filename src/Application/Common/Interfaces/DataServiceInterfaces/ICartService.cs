using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Add;
using Domain.Entities;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface ICartService
    {
        Task<IEnumerable<OfferWithBaseDataDTO>> GetOffersFromCartAsync(long userId);
        Task AddOfferToCartAsync(long offerId, long userId);
        Task RemoveOfferFromCartAsync(long offerId, long userId);
        Task<Cart> GetCartByUser(long userId);
    }
}
