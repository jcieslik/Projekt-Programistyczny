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
        Task<BidDTO> GetBidByIdAsync(long id);
        Task<IEnumerable<BidDTO>> GetBidsFromOfferAsync(long offerId, bool onlyNotHidden);
        Task<IEnumerable<BidDTO>> GetBidsFromUserAsync(long userId, bool onlyNotHidden);
        Task<BidDTO> UpdateBidAsync(UpdateBidDTO dto);
    }
}