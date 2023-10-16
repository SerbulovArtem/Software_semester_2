using ActionManager.DTO;
using DAL.Data.Startup;
using Microsoft.EntityFrameworkCore;
using System;

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

    public void CreateTblAction(TblAction tblAction)
    {
        TblActions.Add(tblAction);
        SaveChanges();
    }

    public void DeleteTblAction(TblAction tblAction)
    {
        TblActions.Remove(tblAction);
        SaveChanges();
    }

    public void UpdateTblAction(int actionId, decimal discountPercentage, int typeactionid)
    {
        var typeaction = TblTypeActions.Find(typeactionid);
        var action = TblActions.Find(actionId);

        if (action != null)
        {
            action.DiscountPercentage = discountPercentage;
            action.TypeActionId = typeactionid;
            action.TypeAction = typeaction;
            action.UpdateTime = DateTime.Now;

            Entry(action).State = EntityState.Modified;
            SaveChanges();
        }
        else
        {
            throw new Exception();
        }
    }
}
