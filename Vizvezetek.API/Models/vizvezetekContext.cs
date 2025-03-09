// Models/VizvezetekContext.cs
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Vizvezetek.API.Models;

public partial class vizvezetekContext : DbContext
{
    public vizvezetekContext()
    {
    }

    public vizvezetekContext(DbContextOptions<vizvezetekContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Hely> hely { get; set; }

    public virtual DbSet<Munkalap> munkalap { get; set; }

    public virtual DbSet<Szerelo> szerelo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Csak akkor állítjuk be, ha még nincs konfigurálva
        if (!optionsBuilder.IsConfigured)
        {
            // Ez csak biztonsági mentés, a Program.cs-ben a DI konténeren keresztül
            // adjuk át a connection string-et
            optionsBuilder.UseMySql("server=localhost;user=root;database=vizvezetek",
                ServerVersion.Parse("10.4.32-mariadb"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Hely>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");
        });

        modelBuilder.Entity<Munkalap>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.HasOne(d => d.hely).WithMany(p => p.munkalap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("munkalap_ibfk_1");

            entity.HasOne(d => d.szerelo).WithMany(p => p.munkalap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("munkalap_ibfk_2");
        });

        modelBuilder.Entity<Szerelo>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
