using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Mapping
{
    public class CarMap : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable(@"Cars", @"dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BrandId).IsRequired();
            builder.Property(x => x.ColorId).IsRequired();
            builder.Property(x => x.ModelYear).HasMaxLength(DateTime.Now.Year + 1);
            builder.Property(x => x.Description).HasMaxLength(int.MaxValue);
        }
    }
}
