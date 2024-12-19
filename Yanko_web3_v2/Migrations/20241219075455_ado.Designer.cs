﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yanko_web3_v2.Models;

#nullable disable

namespace Yanko_web3_v2.Migrations
{
    [DbContext(typeof(PractDbContext))]
    [Migration("20241219075455_ado")]
    partial class ado
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Yanko_web3_v2.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplasedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplasedByToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevoketById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountUserId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.AdvertisementTable", b =>
                {
                    b.Property<int>("AdvertisementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("advertisement_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdvertisementId"));

                    b.Property<int>("ChannelId")
                        .HasColumnType("int")
                        .HasColumnName("channel_id");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int")
                        .HasColumnName("object_id");

                    b.Property<double?>("Price")
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<int?>("Sale")
                        .HasColumnType("int")
                        .HasColumnName("sale");

                    b.Property<int>("TegId")
                        .HasColumnType("int")
                        .HasColumnName("teg_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("AdvertisementId");

                    b.HasIndex("ChannelId");

                    b.HasIndex("ObjectId");

                    b.HasIndex("TegId");

                    b.ToTable("Advertisement_table", (string)null);
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.ChannelTable", b =>
                {
                    b.Property<int>("ChannelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("channel_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChannelId"));

                    b.Property<int>("AutorId")
                        .HasColumnType("int")
                        .HasColumnName("autor_id");

                    b.Property<string>("ChannelName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("channel_name");

                    b.HasKey("ChannelId");

                    b.HasIndex("AutorId");

                    b.ToTable("Channel_table", (string)null);
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.CollectionTable", b =>
                {
                    b.Property<int>("CollectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("collection_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollectionId"));

                    b.Property<string>("CollectionName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("collection_name");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("CollectionId");

                    b.HasIndex("UserId");

                    b.ToTable("Collection_table", (string)null);
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.CommentTable", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("comment_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"));

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("comment_text");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int")
                        .HasColumnName("object_id");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("CommentId")
                        .HasName("PK_Comment_text");

                    b.HasIndex("ObjectId");

                    b.ToTable("Comment_table", (string)null);
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.ImageTable", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("image_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("image")
                        .HasColumnName("image");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int")
                        .HasColumnName("object_id");

                    b.HasKey("ImageId");

                    b.HasIndex("ObjectId");

                    b.ToTable("Image_table", (string)null);
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.ObjectTable", b =>
                {
                    b.Property<int>("ObjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Object_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ObjectId"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    b.Property<int?>("CollectionId")
                        .HasColumnType("int")
                        .HasColumnName("collection_id");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ObjectDescription")
                        .HasColumnType("text")
                        .HasColumnName("Object_description");

                    b.HasKey("ObjectId");

                    b.HasIndex("CollectionId");

                    b.ToTable("Object_table", (string)null);
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.SubscriptionsTable", b =>
                {
                    b.Property<int>("ChannelId")
                        .HasColumnType("int")
                        .HasColumnName("channel_id");

                    b.Property<string>("SubscribersCount")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("subscribers_count")
                        .IsFixedLength();

                    b.Property<int>("SubscriptionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("subscriptions_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionsId"));

                    b.Property<int>("SubscriptionsLevel")
                        .HasColumnType("int")
                        .HasColumnName("subscriptions_level");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions_table", (string)null);
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.TagTable", b =>
                {
                    b.Property<int>("TegId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("teg_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TegId"));

                    b.Property<string>("TegName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("teg_name");

                    b.HasKey("TegId")
                        .HasName("PK_Table_1");

                    b.ToTable("Tag_table", (string)null);
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.TransactionTable", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("transaction_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("RecipientId")
                        .HasColumnType("int")
                        .HasColumnName("recipient_id");

                    b.Property<int>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("sender_id");

                    b.Property<double>("Sum")
                        .HasColumnType("float")
                        .HasColumnName("sum");

                    b.HasKey("TransactionId");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Transaction_table", (string)null);
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.UserTable", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool>("AcceptTerms")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("email");

                    b.Property<DateTime?>("PassordReset")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("password");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.Property<string>("VerificationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Verified")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("User_table", (string)null);
                });

            modelBuilder.Entity("Yanko_web3_v2.Entities.RefreshToken", b =>
                {
                    b.HasOne("Yanko_web3_v2.Models.UserTable", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.AdvertisementTable", b =>
                {
                    b.HasOne("Yanko_web3_v2.Models.ChannelTable", "Channel")
                        .WithMany("AdvertisementTables")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Advertisement_table_Channel_table");

                    b.HasOne("Yanko_web3_v2.Models.ObjectTable", "Object")
                        .WithMany("AdvertisementTables")
                        .HasForeignKey("ObjectId")
                        .IsRequired()
                        .HasConstraintName("FK_Advertisement_table_Object_table");

                    b.HasOne("Yanko_web3_v2.Models.TagTable", "Teg")
                        .WithMany("AdvertisementTables")
                        .HasForeignKey("TegId")
                        .IsRequired()
                        .HasConstraintName("FK_Advertisement_table_Table_1");

                    b.Navigation("Channel");

                    b.Navigation("Object");

                    b.Navigation("Teg");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.ChannelTable", b =>
                {
                    b.HasOne("Yanko_web3_v2.Models.UserTable", "Autor")
                        .WithMany("ChannelTables")
                        .HasForeignKey("AutorId")
                        .IsRequired()
                        .HasConstraintName("FK_Channel_table_User_table");

                    b.Navigation("Autor");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.CollectionTable", b =>
                {
                    b.HasOne("Yanko_web3_v2.Models.UserTable", "User")
                        .WithMany("CollectionTables")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Collection_table_User_table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.CommentTable", b =>
                {
                    b.HasOne("Yanko_web3_v2.Models.ObjectTable", "Object")
                        .WithMany("CommentTables")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Comment_table_Object_table");

                    b.Navigation("Object");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.ImageTable", b =>
                {
                    b.HasOne("Yanko_web3_v2.Models.ObjectTable", "Object")
                        .WithMany("ImageTables")
                        .HasForeignKey("ObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Image_table_Object_table");

                    b.Navigation("Object");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.ObjectTable", b =>
                {
                    b.HasOne("Yanko_web3_v2.Models.CollectionTable", "Collection")
                        .WithMany("ObjectTables")
                        .HasForeignKey("CollectionId")
                        .HasConstraintName("FK_Object_table_Collection_table");

                    b.Navigation("Collection");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.SubscriptionsTable", b =>
                {
                    b.HasOne("Yanko_web3_v2.Models.UserTable", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Subscriptions_table_User_table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.TransactionTable", b =>
                {
                    b.HasOne("Yanko_web3_v2.Models.UserTable", "Recipient")
                        .WithMany("TransactionTableRecipients")
                        .HasForeignKey("RecipientId")
                        .IsRequired()
                        .HasConstraintName("FK_Transaction_table_User_table");

                    b.HasOne("Yanko_web3_v2.Models.UserTable", "Sender")
                        .WithMany("TransactionTableSenders")
                        .HasForeignKey("SenderId")
                        .IsRequired()
                        .HasConstraintName("FK_Transaction_table_User_table1");

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.ChannelTable", b =>
                {
                    b.Navigation("AdvertisementTables");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.CollectionTable", b =>
                {
                    b.Navigation("ObjectTables");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.ObjectTable", b =>
                {
                    b.Navigation("AdvertisementTables");

                    b.Navigation("CommentTables");

                    b.Navigation("ImageTables");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.TagTable", b =>
                {
                    b.Navigation("AdvertisementTables");
                });

            modelBuilder.Entity("Yanko_web3_v2.Models.UserTable", b =>
                {
                    b.Navigation("ChannelTables");

                    b.Navigation("CollectionTables");

                    b.Navigation("RefreshTokens");

                    b.Navigation("TransactionTableRecipients");

                    b.Navigation("TransactionTableSenders");
                });
#pragma warning restore 612, 618
        }
    }
}
