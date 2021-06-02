using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.Comments)
                .WithOne(c => c.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Rates)
                .WithOne(r => r.Customer)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.Customer);

            builder.HasMany(u => u.SoldOffers)
                .WithOne(o => o.Seller);

            builder.HasMany(u => u.Bids)
                .WithOne(b => b.Bidder);

            builder.HasMany(u => u.ReceivedMessages)
                .WithOne(m => m.Recipient);

            builder.HasMany(u => u.SendMessages)
                .WithOne(m => m.Sender);

            builder.HasData(
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Username = "admin",
                        IsActive = true,
                        Password = "admin",
                        Email = "example@example.com",
                        Role = UserRole.Admin
                    }
                );
        }
    }
}
