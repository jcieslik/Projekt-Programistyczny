using Application.DAL.DTO;
using Application.DAL.DTO.CommandDTOs.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface ICityService
    {
        Task<CityDTO> GetCityByIdAsync(long id);
        Task<CityDTO> CreateCityAsync(string name);
        Task<IEnumerable<CityDTO>> GetCitiesAsync();
        Task<CityDTO> UpdateCityAsync(UpdateCityDTO dto);
    }
}
