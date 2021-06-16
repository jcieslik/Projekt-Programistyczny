using Application.DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDTO>> GetProductCategoriesAsync();
        Task<ProductCategoryDTO> CreateProductCategoryAsync(ProductCategoryDTO dto);
    }
}
