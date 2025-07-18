﻿using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data.Configurations
{
    class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasData(
                new League
                {
                    Id = 1,
                    Name = "Premier League",
                }, new League
                {
                    Id = 2,
                    Name = "La Liga",
                });
        }
    }
}
