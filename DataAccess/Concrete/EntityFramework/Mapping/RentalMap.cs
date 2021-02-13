using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Mapping
{
    public class RentalMap : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable(@"Rentals", @"dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CarId).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.RentDate).IsRequired();
        }
    }
}
