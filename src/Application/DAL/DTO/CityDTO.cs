using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class CityDTO : EntityDTO, IMapFrom<City>
    {
        public string Name { get; set; }
    }
}
