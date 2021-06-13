using Application.DAL.DTO;
using Application.DAL.Models;
using Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.DataServiceInterfaces
{
    public interface IFilterService
    {
        Task<IEnumerable<BrandDTO>> GetBrandsAsync();
        Task<IEnumerable<CityDTO>> GetCitiesAsync();
        Task<IEnumerable<ProvinceDTO>> GetProvincessAsync();
        Task<IEnumerable<OfferType>> GetOfferTypesAsync();
        Task<IEnumerable<ProductState>> GetProductStatesAsync();
        Task<FilterVm> GetFiltersData();
    }
}
