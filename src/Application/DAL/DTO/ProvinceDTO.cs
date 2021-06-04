using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class ProvinceDTO : EntityDTO, IMapFrom<Province>
    {
        public string Name { get; set; }
    }
}
