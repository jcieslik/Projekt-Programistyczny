using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Create;
using Application.DAL.DTO.CommandDTOs.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IProductCategoryService
    {
        Task<ProductCategoryDTO> GetProductCategoryByIdAsync(long id);
        Task<IEnumerable<ProductCategoryDTO>> GetProductCategoriesAsync();
        Task<ProductCategoryDTO> CreateProductCategoryAsync(CreateProductCategoryDTO dto);
        Task<ProductCategoryDTO> UpdateProductCategoryAsync(UpdateProductCategoryDTO dto);
    }
}
