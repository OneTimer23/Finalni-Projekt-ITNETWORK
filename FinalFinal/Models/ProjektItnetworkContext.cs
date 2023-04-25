using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace FinalFinal.Models;

public partial class ProjektItnetworkContext : IdentityDbContext<IdentityUser>
{
   
    
    public ProjektItnetworkContext(DbContextOptions<ProjektItnetworkContext> options)
        : base(options)
    {
    }
   
    public virtual DbSet<Insured> Insureds { get; set; }

    public virtual DbSet<Kontakt> Kontakts { get; set; }

    public virtual DbSet<Pojisteni> Pojistenis { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ProjektITNETWORK;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Insured>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Kontakt>(entity =>
        {
            entity.ToTable("Kontakt");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Jmeno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mesto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Prijmeni)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SmerovaciCislo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TelCislo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ulice)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pojisteni>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07A4586992");

            entity.ToTable("Pojisteni");

            entity.Property(e => e.PlatnostDo).HasColumnType("datetime");
            entity.Property(e => e.PlatnostOd).HasColumnType("datetime");
            entity.Property(e => e.PojistenyPredmet)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TypPojistky)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Uzivatel).WithMany(p => p.Pojistenis)
                .HasForeignKey(d => d.UzivatelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pojisteni__Uziva__38996AB5");
        });

       

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
