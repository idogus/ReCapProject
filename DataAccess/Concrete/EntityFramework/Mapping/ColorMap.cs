using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Mapping
{
    public class ColorMap : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable(@"Colors", @"dbo");
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Cars);

            builder.Property(x => x.Name).HasMaxLength(50);
        }
    }
}
