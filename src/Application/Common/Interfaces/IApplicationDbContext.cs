using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Bid> Bids { get; set; }
        DbSet<ProductCategory> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<ProductImage> Images { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Offer> Offers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Province> Provinces { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Wish> Wishes { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<OfferAndDeliveryMethod> OffersAndDeliveryMethods { get; set; }
        DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        DbSet<MessageTransmission> MessageTransmissions { get; set; }
        DbSet<CartOffer> CartOffer { get; set; }
        DbSet<MessageUser> MessageUser { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}