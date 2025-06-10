using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Repository;

public class DatabaseContext : DbContext // Ensure DatabaseContext inherits from DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { } // This constructor now correctly calls the base class constructor

    // Define DbSets for your entities here, e.g.:

    public virtual DbSet<LabelEntity> Labels { get; set; }

    public virtual DbSet<NoteEntity> Notes { get; set; }
    public virtual DbSet<UsersKeepEntity> UsersKeep { get; set; }
    public virtual DbSet<NotesLabelEntity> NotesLabels { get; set; }
    public virtual DbSet<RefreshTokenEntity> UserRefreshTokens { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);



        //modelBuilder.Entity<NoteLabelEntity>()
        //        .HasKey(nl => new { nl.NoteId, nl.LabelId });

        //modelBuilder.Entity<NoteLabelEntity>()
        //   .HasOne(nl => nl.Note)
        //   .WithMany(n => n.NotesLabel)
        //   .HasForeignKey(nl => nl.NoteId);

        //modelBuilder.Entity<NoteLabelEntity>()
        //    .HasOne(nl => nl.Label) 
        //    .WithMany(l => l.NotesLabel) 
        //    .HasForeignKey(nl => nl.LabelId);

        modelBuilder.Entity<RefreshTokenEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("NOW()");
            entity.Property(e => e.IsActive)
            .HasDefaultValue(true);
            entity.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => e.Token)
            .IsUnique();
        });





        modelBuilder.Entity<LabelEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Labels__3213E83F9116D622");

            entity.HasIndex(e => new { e.UserId, e.Name, e.IsActive }, "UniqueActiveLabelPerUser").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("createdAt");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.User).WithMany(p => p.Labels)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Labels__UserId__40058253");
        });


        modelBuilder.Entity<NoteEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notes__3213E83F66741E52");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("createdAt");
            entity.Property(e => e.IsArchived)
                .HasDefaultValue(false)
                .HasColumnName("isArchived");
            entity.Property(e => e.IsPinned)
                .HasDefaultValue(false)
                .HasColumnName("isPinned");
            entity.Property(e => e.IsTrashed)
                .HasDefaultValue(false)
                .HasColumnName("isTrashed");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Notes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notes__userId__3864608B");
            entity.Property(e => e.DeleteForever)
                .HasDefaultValue(false);
        });

        modelBuilder.Entity<NotesLabelEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NotesLab__3213E83F5244A2E5");

            entity.ToTable("NotesLabel");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LabelId).HasColumnName("labelId");
            entity.Property(e => e.NoteId).HasColumnName("noteId");

            entity.HasOne(d => d.Label).WithMany(p => p.NotesLabels)
                .HasForeignKey(d => d.LabelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NotesLabe__label__6AEFE058");

            entity.HasOne(d => d.Note).WithMany(p => p.NotesLabels)
                .HasForeignKey(d => d.NoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NotesLabe__noteI__69FBBC1F");

            entity.HasOne(d => d.User).WithMany(p => p.NotesLabels)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NotesLabel__UsersKeep__on__UserId");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);
        });

        modelBuilder.Entity<UsersKeepEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersKee__3214EC07AFD68A99");

            entity.ToTable("UsersKeep");

            entity.HasIndex(e => e.Email, "UQ__UsersKeep__email").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("lastName");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("passwordHash");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("NOW()")
                .HasColumnName("updatedAt");
            entity.Property(e => e.IsActive)
                .HasColumnName("isActive");
        });

    }

}