using Domain;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class RegistryContext : DbContext
    {
        public RegistryContext(DbContextOptions<RegistryContext> options)
            : base(options) { }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Country>(entity =>
                {
                    entity.ToTable("Countries");

                    entity.HasKey(e => e.Id)
                        .HasName("PK_dbo.Countries");

                    entity.Property(e => e.Name)
                    .IsRequired();
                });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_dbo.Provinces");

                entity.HasIndex(e => e.CountryId, "IX_CountryId");

                entity.Property(e => e.Name)
                .IsRequired();

                entity.HasOne(e => e.Country)
                    .WithMany(e => e.Provinces)
                    .HasForeignKey(e => e.CountryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_dbo.Provinces_dbo.CountryId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_dbo.Users");

                entity.HasIndex(e => e.CountryId, "IX_CountryId");

                entity.HasIndex(e => e.ProvinceId, "IX_ProvinceId");

                entity.Property(e => e.Email)
                .IsRequired();

                entity.Property(e => e.Password)
                .IsRequired();

                entity.HasOne(e => e.Country)
                    .WithMany(e => e.Users)
                    .HasForeignKey(e => e.CountryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_dbo.Users_dbo.CountryId");

                entity.HasOne(e => e.Province)
                    .WithMany(e => e.Users)
                    .HasForeignKey(e => e.ProvinceId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_dbo.Users_dbo.ProvinceId");

                entity.HasIndex(e => e.Email)
                    .IsUnique();
            });


            modelBuilder.Entity<Country>()
                .HasData(new Country { Id = 1, Name = "Afghanistan" },
                        new Country { Id = 2, Name = "France" },
                        new Country { Id = 3, Name = "Greece" },
                        new Country { Id = 4, Name = "Hungary" },
                        new Country { Id = 5, Name = "Tanzania" });

            modelBuilder.Entity<Province>()
                .HasData(new Province { Id = 1, Name = "Badakhshan", CountryId = 1 },
                        new Province { Id = 2, Name = "Farah", CountryId = 1 },
                        new Province { Id = 3, Name = "Ghazni", CountryId = 1 },
                        new Province { Id = 4, Name = "Ghōr", CountryId = 1 },
                        new Province { Id = 5, Name = "Métropole de Lyon", CountryId = 2 },
                        new Province { Id = 6, Name = "Normandie", CountryId = 2 },
                        new Province { Id = 7, Name = "Nógrád County", CountryId = 4 },
                        new Province { Id = 8, Name = "Pécs", CountryId = 4 },
                        new Province { Id = 9, Name = "Somogy County", CountryId = 4 },
                        new Province { Id = 10, Name = "Shinyanga", CountryId = 5 });
        }
    }
}
