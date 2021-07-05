namespace Application.DAL.DTO.CommandDTOs.Add
{
    public class AddOfferToCartDTO
    {
        public long OfferId { get; set; }
        public long CartId { get; set; }
        public int ProductsCount { get; set; }
    }
}
