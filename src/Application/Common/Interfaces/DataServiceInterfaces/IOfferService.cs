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
        Task<PaginatedList<OfferWithBaseDataDTO>> GetPaginatedOffersFromUserActiveWishesAsync(long userId, FilterModel filterModel, PaginationProperties paginationProperties);
        Task<PaginatedList<OfferWithBaseDataDTO>> GetPaginatedOffersAsync(FilterModel filterModel, PaginationProperties paginationProperties);
        Task<OfferDTO> UpdateOfferAsync(UpdateOfferDTO dto);
        Task ChangeStatusOfOffersAfterEndDate();
        Task<PaginatedList<OfferWithBaseDataDTO>> GetUserAciveBidOffers(long userId, PaginationProperties paginationProperties);
    }
}
