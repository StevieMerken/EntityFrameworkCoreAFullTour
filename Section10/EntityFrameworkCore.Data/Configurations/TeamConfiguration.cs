using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Data.Configurations
{
    class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasIndex(t => t.Name).IsUnique();
            //builder.HasIndex(q => new { q.CoachId, q.LeagueId }).IsUnique();

            //builder.Property(t => t.Name)
            //    .HasMaxLength(100)
            //    .IsRequired();

            builder.HasMany(m => m.HomeMatches)
                .WithOne(t => t.HomeTeam)
                .HasForeignKey(t => t.HomeTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.AwayMatches)
                .WithOne(t => t.AwayTeam)
                .HasForeignKey(t => t.AwayTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Team
                {
                    Id = 1,
                    Name = "Team A",
                    DateCreated = new DateTime(2025, 5, 29, 13, 34, 26, 346, DateTimeKind.Unspecified).AddTicks(2056),
                    LeagueId = 1,
                    CoachId = 1,
                },
                new Team
                {
                    Id = 2,
                    Name = "Team B",
                    DateCreated = new DateTime(2025, 5, 29, 13, 34, 26, 346, DateTimeKind.Unspecified).AddTicks(2056),
                    LeagueId = 1,
                    CoachId = 2,
                });
        }
    }
}
