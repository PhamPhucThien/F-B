﻿using FooDrink.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooDrink.Database.Configuration
{
    public class MenuConfig : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            _ = builder.HasMany(menu => menu.Products)
                   .WithOne(product => product.Menu);
            _ = builder.HasOne(menu => menu.Restaurant)
                   .WithOne(res => res.Menu);
        }
    }
}
