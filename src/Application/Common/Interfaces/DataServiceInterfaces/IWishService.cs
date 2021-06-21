using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IWishService
    {
        Task<WishDTO> CreateWishAsync(CreateWishDto dto);
        Task HideWish(long id);
    }
}