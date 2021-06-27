using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;

namespace Application.DAL.DTO
{
    public class DeliveryDTO : EntityDTO, IMapFrom<DeliveryMethod>
    {
        public string Name { get; set; }
        public double BasePrice { get; set; }
    }
}
