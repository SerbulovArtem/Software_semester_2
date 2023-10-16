using ActionManager.DTO;
using DAL.Data.Startup;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Collections.Specialized.BitVector32;

namespace ActionManager.DAL.Data;

public partial class ImdbContext : DbContext
{
    private int _type;
    public ImdbContext(int type)
    {
        _type = type;
    }

    public ImdbContext(DbContextOptions<ImdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAction> TblActions { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblTypeAction> TblTypeActions { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Startup.GetConnectionString(_type));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAction>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.TblActions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblActions_tblProduct");

            entity.HasOne(d => d.TypeAction).WithMany(p => p.TblActions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblActions_tblTypeAction");
        });

        modelBuilder.Entity<TblTypeAction>(entity =>
        {
            entity.HasKey(e => e.TypeActionId).HasName("PK_tblTypeAction");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
