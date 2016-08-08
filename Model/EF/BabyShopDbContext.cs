using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
namespace Model.EF
{
    public class BabyShopDbContext : DbContext
    {
        public BabyShopDbContext() : base("BabyShopConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<ProductCategory> CategoryProducts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }
        public virtual DbSet<PostTag> PostTags { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductTag> ProductTags { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<ProductSize> ProductSizes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
