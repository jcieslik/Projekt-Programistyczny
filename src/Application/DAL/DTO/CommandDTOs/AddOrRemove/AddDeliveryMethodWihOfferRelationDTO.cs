namespace Application.DAL.DTO.CommandDTOs.AddOrRemove
{
    public class AddDeliveryMethodWihOfferRelationDTO
    {
        public long DeliveryMethodId { get; set; }
        public long OfferId { get; set; }
        public string DeliveryMethodName { get; set; }
        public double DeliveryFullPrice { get; set; }
    }
}
