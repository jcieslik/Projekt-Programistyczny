using Application.Common.Dto;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.DAL.DTO
{
    public class ProductCategoryDTO : EntityDTO, IMapFrom<ProductCategory>
    {
        public string Name { get; set; }
        public Guid ParentCategoryId { get; set; }
        public ICollection<ProductCategoryDTO> ChildrenCategories { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductCategory, ProductCategoryDTO>()
                .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategory.Id));
        }
    }
}
