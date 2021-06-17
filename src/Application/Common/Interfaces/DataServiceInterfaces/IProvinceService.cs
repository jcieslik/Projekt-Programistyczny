using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Update;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IProvinceService
    {
        Task<ProvinceDTO> GetProvinceByIdAsync(long id);
        Task<ProvinceDTO> CreateProvinceAsync(string name);
        Task<IEnumerable<ProvinceDTO>> GetProvincesAsync();
        Task<ProvinceDTO> UpdateProvinceAsync(UpdateProvinceDTO dto);
    }
}
