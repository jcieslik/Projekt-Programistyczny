using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IProductImageService
    {
        Task<ProductImageDTO> GetProductImageByIdAsync(Guid id);
        Task<IEnumerable<ProductImageDTO>> GetProductImagesFromOfferdAsync(Guid offerId, bool onlyNotHidden);
        Task<ProductImageDTO> CreateProductImageAsync(CreateProductImageDTO dto);
        Task<ProductImageDTO> UpdateProductImageAsync(UpdateProductImageDTO dto);
    }
}
