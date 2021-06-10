﻿using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IRateService
    {
        Task<ProductRateDTO> GetRateByIdAsync(Guid id);
        Task<IEnumerable<ProductRateDTO>> GetRatesFromUserAsync(Guid userId);
        Task<IEnumerable<ProductRateDTO>> GetRatesFromOfferAsync(Guid offerId);
        Task<ProductRateDTO> CreateRateAsync(CreateProductRateDTO dto);
        Task<ProductRateDTO> UpdateRateAsync(UpdateProductRateDTO dto);
    }
}