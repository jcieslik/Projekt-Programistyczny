using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IRateService
    {
        Task<ProductRateDTO> GetRateByIdAsync(long id);
        Task<IEnumerable<ProductRateDTO>> GetRatesFromUserAsync(long userId, bool onlyNotHidden = true);
        Task<IEnumerable<ProductRateDTO>> GetRatesFromOfferAsync(long offerId, bool onlyNotHidden = true);
        Task<ProductRateDTO> CreateRateAsync(CreateProductRateDTO dto);
        Task<ProductRateDTO> UpdateRateAsync(UpdateProductRateDTO dto);
    }
}
