using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace server.Models.DB;

public partial class BookStoreDbContext : DbContext
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    // public virtual DbSet<CartItem> { get; set;}
    public virtual DbSet<CartItem> CartIterms { get; set; }
    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }



    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

   
    public virtual DbSet<User> Users { get; set; }

   

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BookStoreDB;User ID=sa;Password=SqlServer@2277;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__86516BCFA24EBAF7");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("author_name");
            entity.Property(e => e.Bio)
                .HasColumnType("text")
                .HasColumnName("bio");
        });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookId).HasName("PK__Books__490D1AE1A84A3ED0");

                entity.Property(e => e.BookId).HasColumnName("book_id");
                entity.Property(e => e.AuthorId).HasColumnName("author_id");
                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("image_url");
                entity.Property(e => e.Language)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("language");
                entity.Property(e => e.PagesCount).HasColumnName("pages_count");
                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");
                entity.Property(e => e.PublishedDate).HasColumnName("published_date");
                entity.Property(e => e.PublishedVersion).HasColumnName("published_version");
                entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.HasOne(d => d.Author).WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK__Books__author_id__3B75D760");

                entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK__Books__publisher__3C69FB99");

                entity.HasMany(d => d.Tags).WithMany(p => p.Books)
                    .UsingEntity<Dictionary<string, object>>(
                        "BookTag",
                        r => r.HasOne<Tag>().WithMany()
                            .HasForeignKey("TagId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__BookTag__tag_id__44FF419A"),
                        l => l.HasOne<Book>().WithMany()
                            .HasForeignKey("BookId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__BookTag__book_id__440B1D61"),
                        j =>
                        {
                            j.HasKey("BookId", "TagId").HasName("PK__BookTag__3D2470CACC5A9021");
                            j.ToTable("BookTag");
                            j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                            j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                        });
            });

       modelBuilder.Entity<Cart>(entity =>
{
    entity.HasKey(e => e.CartId).HasName("PK__Carts__2EF52A27B9CE3939");

    entity.Property(e => e.CartId).HasColumnName("cart_id");
   
    entity.Property(e => e.UserId).HasColumnName("user_id");

 

    entity.HasOne(d => d.User).WithMany(p => p.Carts)
        .HasForeignKey(d => d.UserId)
        .HasConstraintName("FK__Carts__user_id__5441852A");

    entity.HasMany(d => d.CartItems).WithOne(p => p.Cart)
        .HasForeignKey(d => d.CartId)
        .HasConstraintName("FK__CartItems__cart_id__12345678");
});


        modelBuilder.Entity<CartItem>(entity =>
{
    entity.HasKey(e => e.CartItemId).HasName("PK__CartItems__12345678");

    entity.Property(e => e.CartItemId).HasColumnName("cart_item_id");
    entity.Property(e => e.CartId).HasColumnName("cart_id");
    entity.Property(e => e.BookId).HasColumnName("book_id");
    entity.Property(e => e.Quantity).HasColumnName("quantity");

    entity.HasOne(d => d.Book).WithMany(p => p.CartItems)
        .HasForeignKey(d => d.BookId)
        .HasConstraintName("FK__CartItems__book_id__12345678");

    entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
        .HasForeignKey(d => d.CartId)
        .HasConstraintName("FK__CartItems__cart_id__12345678");
});



        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__465962298B4F3117");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Orders__payment___4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__user_id__4AB81AF0");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__Order_it__3764B6BC850F45D3");

            entity.ToTable("Order_items");

            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Order_ite__book___5812160E");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Order_ite__order__571DF1D5");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__ED1FC9EA0B9BA1AD");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("payment_type");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__3263F29DAFF3A768");

            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.PublisherAddress)
                .HasColumnType("text")
                .HasColumnName("publisher_address");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("publisher_name");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__60883D90FB2C6292");

            entity.Property(e => e.ReviewId).HasColumnName("review_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Review1)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("review");
            entity.Property(e => e.ReviewedDate).HasColumnName("reviewed_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Reviews__book_id__4E88ABD4");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__user_id__4F7CD00D");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tags__4296A2B663E5BFA0");

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Tag1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tag");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F0B2D23E4");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Contact)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("contact");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PinCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("pin_code");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
