using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Add;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IOfferService
    {
        Task<OfferDTO> CreateOfferAsync(CreateOfferDTO dto);
        Task<OfferDTO> GetOfferByIdAsync(long id);
        Task<IEnumerable<OfferWithBaseDataDTO>> GetOffersFromUserAsync(long id);
        Task<PaginatedList<OfferWithBaseDataDTO>> GetPaginatedOffersFromUserActiveWishesAsync(long userId, PaginationProperties paginationProperties);
        Task<PaginatedList<OfferWithBaseDataDTO>> GetPaginatedOffersAsync(FilterModel filterModel, PaginationProperties paginationProperties);
        Task<OfferDTO> UpdateOfferAsync(UpdateOfferDTO dto);
        Task<IEnumerable<OfferWithBaseDataDTO>> GetOffersAsync();
        Task<IEnumerable<OfferWithBaseDataDTO>> GetOffersFromCartAsync(long cartId);
        Task AddOfferToCartAsync(AddOrRemoveOfferToCartDTO dto);
        Task RemoveOfferFromCartAsync(AddOrRemoveOfferToCartDTO dto);
        Task ChangeStatusOfOffersAfterEndDate();
    }
}
