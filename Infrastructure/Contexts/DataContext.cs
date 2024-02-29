using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<CourseAuthorEntity> CourseAuthors { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<ProfilePictureEntity> ProfilePictures { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserSavedItemEntity> UserSavedItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserSavedItemEntity>()
                .HasKey(x => new { x.UserId, x.CourseId });
        }
    }
}
