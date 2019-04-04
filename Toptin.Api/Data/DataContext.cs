using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Toptin.Api.Models;

namespace Toptin.Api.Data
{
    public class DataContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //Models
        public DbSet<KalaAttribute> Attribute { get; set; }
        public DbSet<AttributeCategory> AttributeCategory { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Favorit> Favorit { get; set; }
        public DbSet<Kala> Kala { get; set; }
        public DbSet<KalaPicture> KalaPicture { get; set; }
        public DbSet<KalaRequest> KalaRequest { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<RequestDetail> RequestDetail { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Log> Log { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Question>()
                .HasOne(q => q.ParentQuestion)
                .WithMany(q => q.ParentQuestions)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
