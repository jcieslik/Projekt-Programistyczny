namespace Application.DAL.DTO.CommandDTOs.Create
{
    public class CreateProductCategoryDTO
    {
        public string Name { get; set; }
        public long? ParentCategoryId { get; set; }
    }
}
