using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
            oId => oId.Value,
            dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany<OrderItem>()
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.ComplexProperty(o => o.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.ShippingAddress, shippingAddressBuilder =>
        {
            shippingAddressBuilder.Property(sa => sa.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.LastName)
                .HasMaxLength(100)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.EmailAddress)
                .HasMaxLength(50)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.AddressLine)
                .HasMaxLength(100)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.Country)
                .HasMaxLength(100)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.State)
                .HasMaxLength(25)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.ZipCode)
                .HasMaxLength(10)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.BillingAddress, shippingAddressBuilder =>
        {
            shippingAddressBuilder.Property(sa => sa.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.LastName)
                .HasMaxLength(100)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.EmailAddress)
                .HasMaxLength(50)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.AddressLine)
                .HasMaxLength(100)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.Country)
                .HasMaxLength(100)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.State)
                .HasMaxLength(25)
                .IsRequired();

            shippingAddressBuilder.Property(sa => sa.ZipCode)
                .HasMaxLength(10)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.Payment,
            paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName)
                    .HasMaxLength(50);

                paymentBuilder.Property(p => p.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();

                paymentBuilder.Property(p => p.ExpirationDate)
                    .HasMaxLength(16)
                    .IsRequired();

                paymentBuilder.Property(p => p.Cvv)
                    .HasMaxLength(3);

                paymentBuilder.Property(p => p.PaymentMethod);
            });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => Enum.Parse<OrderStatus>(dbStatus));

        builder.Property(o => o.Total);
    }
}