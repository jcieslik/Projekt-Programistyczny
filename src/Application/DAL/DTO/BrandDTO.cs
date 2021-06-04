using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class BrandDTO : EntityDTO, IMapFrom<Brand>
    {
        public string Name { get; set; }
    }
}
