using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZayiflamaMerkeziTakip.Models;

public partial class ZayiflamaMerkeziTakipContext : DbContext
{
    public ZayiflamaMerkeziTakipContext()
    {
    }

    public ZayiflamaMerkeziTakipContext(DbContextOptions<ZayiflamaMerkeziTakipContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doktorlar> Doktorlars { get; set; }

    public virtual DbSet<HastaTedavileri> HastaTedavileris { get; set; }

    public virtual DbSet<Hastalar> Hastalars { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Olcumler> Olcumlers { get; set; }

    public virtual DbSet<Randevu> Randevus { get; set; }

    public virtual DbSet<Tedaviler> Tedavilers { get; set; }

    public virtual DbSet<Uzmanlik> Uzmanliks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-0GJBLGU\\SQLEXPRESS;Initial Catalog=ZayiflamaMerkeziTakip;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doktorlar>(entity =>
        {
            entity.HasOne(d => d.Uzmanlik).WithMany(p => p.Doktorlars).HasConstraintName("FK_Doktorlar_Uzmanlik");
        });

        modelBuilder.Entity<HastaTedavileri>(entity =>
        {
            entity.HasOne(d => d.Hasta).WithMany(p => p.HastaTedavileris).HasConstraintName("FK_HastaTedavileri_Hastalar");

            entity.HasOne(d => d.Tedavi).WithMany(p => p.HastaTedavileris).HasConstraintName("FK_HastaTedavileri_Tedaviler");
        });

        modelBuilder.Entity<Hastalar>(entity =>
        {
            entity.HasOne(d => d.Tedavi).WithMany(p => p.Hastalars).HasConstraintName("FK_Hastalar_Tedaviler");
        });

        modelBuilder.Entity<Olcumler>(entity =>
        {
            entity.HasOne(d => d.Hasta).WithMany(p => p.Olcumlers).HasConstraintName("FK_Olcumler_Hastalar");
        });

        modelBuilder.Entity<Randevu>(entity =>
        {
            entity.HasOne(d => d.Doktor).WithMany(p => p.Randevus).HasConstraintName("FK_Randevu_Doktorlar");

            entity.HasOne(d => d.Hasta).WithMany(p => p.Randevus).HasConstraintName("FK_Randevu_Hastalar");

            entity.HasOne(d => d.Uzmanlik).WithMany(p => p.Randevus).HasConstraintName("FK_Randevu_Uzmanlik");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
