using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Mapping
{
    public class BrandMap : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable(@"Brands", @"dbo");
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Cars);

            builder.Property(x => x.Name).HasMaxLength(50);
        }
    }
}