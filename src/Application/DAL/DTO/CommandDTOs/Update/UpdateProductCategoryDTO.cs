namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateProductCategoryDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }
    }
}
