using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IBidService
    {
        Task<BidDTO> CreateBidAsync(CreateBidDTO dto);
        Task<BidDTO> GetBidByIdAsync(Guid id);
        Task<IEnumerable<BidDTO>> GetBidsFromOfferAsync(Guid offerId, bool onlyNotHidden);
        Task<IEnumerable<BidDTO>> GetBidsFromUserAsync(Guid userId, bool onlyNotHidden);
        Task<BidDTO> UpdateBidAsync(UpdateBidDTO dto);
    }
}