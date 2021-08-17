using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Comment)
                .WithOne(c => c.Order)
                .HasForeignKey<Order>(c => c.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Offer)
                .WithMany(x => x.Orders);
            builder.HasOne(x => x.DeliveryMethod)
                .WithMany(x => x.Orders)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
