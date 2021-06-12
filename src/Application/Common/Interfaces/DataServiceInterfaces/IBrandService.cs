using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IBrandService
    {
        Task<BrandDTO> CreateBrandAsync(string name);
        Task<IEnumerable<BrandDTO>> GetBrandsAsync();
        Task<BrandDTO> UpdateBrandAsync(UpdateBrandDTO dto);
    }
}