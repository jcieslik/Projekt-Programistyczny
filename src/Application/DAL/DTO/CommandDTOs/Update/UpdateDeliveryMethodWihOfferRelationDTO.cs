namespace Application.DAL.DTO.CommandDTOs.Update
{
    public class UpdateDeliveryMethodWihOfferRelationDTO
    {
        public long Id { get; set; }
        public long DeliveryMethodId { get; set; }
        public long? OfferId { get; set; }
        public double? FullPrice { get; set; }
    }
}
