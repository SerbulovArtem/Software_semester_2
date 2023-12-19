using System;
using System.Collections.Generic;
using ActionManager.DTO;
using ActionManager.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ActionManager.DAL.Data;

public partial class ActionManagerContext : DbContext
{
    private int _type;
    public ActionManagerContext(int type)
    {
        _type = type;
    }

    public ActionManagerContext(DbContextOptions<ActionManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAction> TblActions { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblTypeAction> TblTypeActions { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=ActionManager;User ID=wintermute;Password=WTNLPARg83655;Integrated Security=False;TrustServerCertificate=True;Encrypt=False;MultipleActiveResultSets=true");

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
