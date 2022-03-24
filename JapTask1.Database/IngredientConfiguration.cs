using JapTask1.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Database
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x => x.PurchasedPrice).IsRequired().HasMaxLength(64);
            builder.Property(x => x.PurchasedQuantity).IsRequired().HasMaxLength(64);
            builder.Property(x => x.PurchasedUnitOfMeasure).IsRequired();
        }
    }
}
