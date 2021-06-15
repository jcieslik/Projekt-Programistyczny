using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ProductCategory : AuditableEntity
    {
        [Required]
        public string Name { get; set; }
        public ProductCategory ParentCategory { get; set; }
        public ICollection<Offer> Offers { get; set; }
        public ICollection<ProductCategory> ChildrenCategories { get; set; }

        public bool HasDescendantCategory(long categoryId)
        {
            if(ChildrenCategories.Count == 0)
            {
                return false;
            }
            foreach(var cat in ChildrenCategories)
            {
                if(cat.Id == categoryId)
                {
                    return true;
                }
                var result = cat.HasDescendantCategory(categoryId);
                if (result == true)
                {
                    return result;
                }
            }
            return false;
        }

        public bool HasAncestorCategory(long categoryId)
        {
            if(ParentCategory == null)
            {
                return false;
            }
            if(ParentCategory.Id == categoryId)
            {
                return true;
            }
            return ParentCategory.HasAncestorCategory(categoryId);
        }
    }
}
