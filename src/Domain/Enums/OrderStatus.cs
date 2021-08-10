namespace Domain.Enums
{
    public enum OrderStatus
    {
        AwaitingForPayment = 0,
        Paid = 1,
        InDelivery = 2,
        Delivered = 3,
        Canceled = 4,
        All = 5 
    }
}
