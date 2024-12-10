using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Yanko_web3_v2.Models;

public partial class PractDbContext : DbContext
{
    public PractDbContext()
    {
    }

    public PractDbContext(DbContextOptions<PractDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdvertisementTable> AdvertisementTables { get; set; }

    public virtual DbSet<ChannelTable> ChannelTables { get; set; }

    public virtual DbSet<CollectionTable> CollectionTables { get; set; }

    public virtual DbSet<CommentTable> CommentTables { get; set; }

    public virtual DbSet<ImageTable> ImageTables { get; set; }

    public virtual DbSet<ObjectTable> ObjectTables { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubscriptionsTable> SubscriptionsTables { get; set; }

    public virtual DbSet<TagTable> TagTables { get; set; }

    public virtual DbSet<TransactionTable> TransactionTables { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdvertisementTable>(entity =>
        {
            entity.HasKey(e => e.AdvertisementId);

            entity.ToTable("Advertisement_table");

            entity.Property(e => e.AdvertisementId).HasColumnName("advertisement_id");
            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Sale).HasColumnName("sale");
            entity.Property(e => e.TegId).HasColumnName("teg_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Channel).WithMany(p => p.AdvertisementTables)
                .HasForeignKey(d => d.ChannelId)
                .HasConstraintName("FK_Advertisement_table_Channel_table");

            entity.HasOne(d => d.Object).WithMany(p => p.AdvertisementTables)
                .HasForeignKey(d => d.ObjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Advertisement_table_Object_table");

            entity.HasOne(d => d.Teg).WithMany(p => p.AdvertisementTables)
                .HasForeignKey(d => d.TegId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Advertisement_table_Table_1");
        });

        modelBuilder.Entity<ChannelTable>(entity =>
        {
            entity.HasKey(e => e.ChannelId);

            entity.ToTable("Channel_table");

            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.AutorId).HasColumnName("autor_id");
            entity.Property(e => e.ChannelName)
                .HasColumnType("text")
                .HasColumnName("channel_name");

            entity.HasOne(d => d.Autor).WithMany(p => p.ChannelTables)
                .HasForeignKey(d => d.AutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Channel_table_User_table");
        });

        modelBuilder.Entity<CollectionTable>(entity =>
        {
            entity.HasKey(e => e.CollectionId);

            entity.ToTable("Collection_table");

            entity.Property(e => e.CollectionId).HasColumnName("collection_id");
            entity.Property(e => e.CollectionName)
                .HasColumnType("text")
                .HasColumnName("collection_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.CollectionTables)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Collection_table_User_table");
        });

        modelBuilder.Entity<CommentTable>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK_Comment_text");

            entity.ToTable("Comment_table");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.CommentText)
                .HasColumnType("text")
                .HasColumnName("comment_text");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Object).WithMany(p => p.CommentTables)
                .HasForeignKey(d => d.ObjectId)
                .HasConstraintName("FK_Comment_table_Object_table");
        });

        modelBuilder.Entity<ImageTable>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("Image_table");

            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.Image)
                .HasColumnType("image")
                .HasColumnName("image");
            entity.Property(e => e.ObjectId).HasColumnName("object_id");

            entity.HasOne(d => d.Object).WithMany(p => p.ImageTables)
                .HasForeignKey(d => d.ObjectId)
                .HasConstraintName("FK_Image_table_Object_table");
        });

        modelBuilder.Entity<ObjectTable>(entity =>
        {
            entity.HasKey(e => e.ObjectId);

            entity.ToTable("Object_table");

            entity.Property(e => e.ObjectId).HasColumnName("Object_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CollectionId).HasColumnName("collection_id");
            entity.Property(e => e.Link).HasColumnType("text");
            entity.Property(e => e.ObjectDescription)
                .HasColumnType("text")
                .HasColumnName("Object_description");

            entity.HasOne(d => d.Collection).WithMany(p => p.ObjectTables)
                .HasForeignKey(d => d.CollectionId)
                .HasConstraintName("FK_Object_table_Collection_table");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Role1)
                .HasColumnType("text")
                .HasColumnName("role");
        });

        modelBuilder.Entity<SubscriptionsTable>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Subscriptions_table");

            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.SubscribersCount)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("subscribers_count");
            entity.Property(e => e.SubscriptionsId)
                .ValueGeneratedOnAdd()
                .HasColumnName("subscriptions_id");
            entity.Property(e => e.SubscriptionsLevel).HasColumnName("subscriptions_level");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Subscriptions_table_User_table");
        });

        modelBuilder.Entity<TagTable>(entity =>
        {
            entity.HasKey(e => e.TegId).HasName("PK_Table_1");

            entity.ToTable("Tag_table");

            entity.Property(e => e.TegId).HasColumnName("teg_id");
            entity.Property(e => e.TegName)
                .HasColumnType("text")
                .HasColumnName("teg_name");
        });

        modelBuilder.Entity<TransactionTable>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("Transaction_table");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.RecipientId).HasColumnName("recipient_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.Sum).HasColumnName("sum");

            entity.HasOne(d => d.Recipient).WithMany(p => p.TransactionTableRecipients)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_table_User_table");

            entity.HasOne(d => d.Sender).WithMany(p => p.TransactionTableSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transaction_table_User_table1");
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("User_table");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasColumnType("text")
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasColumnType("text")
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.UserTables)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_User_table_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
