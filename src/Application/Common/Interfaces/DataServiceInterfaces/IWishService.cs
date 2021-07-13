using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IWishService
    {
        Task<WishDTO> CreateWishAsync(CreateWishDto dto);
        Task HideWish(long offerId, long userId);
        Task<bool> CheckForUserWish(long offerId, long userId);
        Task DeleteAsync(long offerId, long userId);
        Task<IEnumerable<long>> GetUserWishesCategoriesIds(long userId);
    }
}