using Application.Common.Dto;
using Application.Common.Mappings;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.DAL.DTO
{
    public class ProductCategoryDTO : EntityDTO, IMapFrom<ProductCategory>
    {
        public string Name { get; set; }
        public ProductCategoryDTO ParentCategory { get; set; }
        public ICollection<OfferDTO> Offers { get; set; }
        public ICollection<ProductCategoryDTO> ChildrenCategories { get; set; }
    }
}
