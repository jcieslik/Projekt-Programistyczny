using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Add;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface ICartService
    {
        Task AddOfferToCartAsync(AddOfferToCartDTO dto);
        Task<IEnumerable<CartOfferDTO>> GetOffersFromCartAsync(long cartId);
        Task RemoveOfferFromCartAsync(long relationId);
        Task<long> Create(long userId);
    }
}
