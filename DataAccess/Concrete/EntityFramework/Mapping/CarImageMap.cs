using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Mapping
{
    public class CarImageMap : IEntityTypeConfiguration<CarImage>
    {
        public void Configure(EntityTypeBuilder<CarImage> builder)
        {
            builder.ToTable(@"CarImages", @"dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CarId).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.ImagePath).HasMaxLength(256);
            builder.Property(x => x.Date).IsRequired();
        }
    }
}
