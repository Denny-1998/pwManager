using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace pwManager.Models;

public partial class PwManagerContext : DbContext
{
    public PwManagerContext()
    {
    }

    public PwManagerContext(DbContextOptions<PwManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=XNXX-PC;Database=pwManager;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BelongsToUser).HasColumnName("belongsToUser");
            entity.Property(e => e.Iv)
                .HasMaxLength(50)
                .HasColumnName("IV");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(512);
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.BelongsToUserNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.BelongsToUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Accounts_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(512)
                .HasColumnName("passwordHash");
            entity.Property(e => e.Salt)
                .HasMaxLength(50)
                .HasColumnName("salt");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
