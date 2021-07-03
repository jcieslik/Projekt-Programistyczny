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

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.Customer);

            builder.HasMany(u => u.SoldOffers)
                .WithOne(o => o.Seller);

            builder.HasMany(u => u.Bids)
                .WithOne(b => b.Bidder);

            builder.HasMany(u => u.Messages)
                .WithOne(m => m.MailboxOwner);

            builder.HasMany(u => u.Wishes)
                .WithOne(w => w.Customer);

            builder.HasOne(u => u.Cart)
                .WithOne(c => c.Customer)
                .HasForeignKey<Cart>(c => c.CustomerId);

            builder.HasOne(u => u.Province)
                .WithMany(x => x.Users)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                    new User
                    {
                        Id = 1,
                        Username = "admin",
                        IsActive = true,
                        Password = "admin",
                        Email = "example@example.com",
                        Role = UserRole.Admin,
                        Name = "Jan",
                        Surname = "Kowalski",
                        City = "Brak",
                        Street = "Brak",
                        PostCode = "Brak"
                        }
                );
        }
    }
}
