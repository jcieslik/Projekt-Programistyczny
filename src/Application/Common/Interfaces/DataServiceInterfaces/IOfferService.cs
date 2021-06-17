using Application.Common.Models;
using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IOfferService
    {
        Task<OfferDTO> CreateOfferAsync(CreateOfferDTO dto);
        Task<OfferDTO> GetOfferByIdAsync(long id);

        Task<IEnumerable<OfferWithBaseDataDTO>> GetOffersFromUserAsync(long id);
        Task<PaginatedList<OfferWithBaseDataDTO>> GetPaginatedOffersAsync(FilterModel filterModel, PaginationProperties paginationProperties);
        Task<OfferDTO> UpdateOfferAsync(UpdateOfferDTO dto);
        Task<IEnumerable<OfferWithBaseDataDTO>> GetOffersAsync();
    }
}