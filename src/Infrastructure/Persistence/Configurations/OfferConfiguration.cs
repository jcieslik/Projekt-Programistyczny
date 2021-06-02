using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.HasMany(o => o.Comments)
                .WithOne(c => c.Offer)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Rates)
                .WithOne(r => r.Offer)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(o => o.Images)
                .WithOne(i => i.Offer)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Orders)
                .WithOne(order => order.Offer);

            builder.HasMany(o => o.Bids)
                .WithOne(b => b.Offer);

            builder.HasOne(o => o.Category)
                .WithMany(c => c.Offers);

            builder.HasOne(o => o.Province)
                .WithMany(p => p.Offers);

            builder.HasOne(o => o.Brand)
                .WithMany(b => b.Offers);

            builder.HasOne(o => o.Seller)
                .WithMany(s => s.SoldOffers);

            builder.HasOne(o => o.City)
                .WithMany(c => c.Offers);

        }
    }
}
