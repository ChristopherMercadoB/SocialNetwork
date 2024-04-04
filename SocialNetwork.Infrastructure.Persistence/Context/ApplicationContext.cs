using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Common;
using SocialNetwork.Core.Domain.Entities;


namespace SocialNetwork.Infrastructure.Persistence.Context
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }

        public async override Task<int> SaveChangesAsync(CancellationToken token = new())
        {
            foreach (var item in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedBy = "Chris";
                        item.Entity.CreatedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        item.Entity.CreatedBy = "Chris";
                        item.Entity.CreatedDate = DateTime.Now;
                        break;
                }
            }
            return await base.SaveChangesAsync(token);
        }

        public DbSet<FriendShip> Friends { get; set; }
        public DbSet<Post> Publications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            #region Tables
            model.Entity<FriendShip>().ToTable("Amigos");
            model.Entity<Post>().ToTable("Publicaciones");
            model.Entity<Comment>().ToTable("Comentarios");
            model.Entity<User>().ToTable("Usuario");
            #endregion

            #region Primary Keys
            model.Entity<FriendShip>().HasKey(x=>x.Id);
            model.Entity<Post>().HasKey(x => x.Id);
            model.Entity<Comment>().HasKey(x => x.Id);
            model.Entity<User>().HasKey(x => x.Id);
            #endregion

            #region Foreign Keys

            model.Entity<User>()
                .HasMany<Post>(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);               

            model.Entity<User>()
                .HasMany<Comment>(u=>u.Comments)
                .WithOne(c=>c.User)
                .HasForeignKey(c=>c.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            model.Entity<User>()
                .HasMany<FriendShip>(u=>u.Friends)
                .WithOne(p=>p.UserFriend)
                .HasForeignKey(f=>f.UserFriendId)
                .OnDelete(DeleteBehavior.Cascade);

            model.Entity<Post>()
                .HasMany<Comment>(p=>p.Comments)
                .WithOne(p=>p.Post)
                .HasForeignKey(p=>p.PostId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            model.Entity<Comment>()
                .HasMany<Reply>(c=>c.Replies)
                .WithOne(r=>r.Comment)
                .HasForeignKey(r=>r.CommentId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            #endregion

            #region Properties
            #region Friend
            model.Entity<FriendShip>()
                .Property(p => p.CurrentUserId)
                .IsRequired();
            #endregion

            #region Post
            model.Entity<Post>()
                .Property(p=>p.Description)
                .IsRequired();

            model.Entity<Post>()
                .Property(p => p.ImageUrl)
                .IsRequired(false);

            model.Entity<Post>()
                .Property(p => p.VideoUrl)
                .IsRequired(false);
            #endregion

            #region Comment

            model.Entity<Comment>()
                .Property(p => p.Description)
                .IsRequired();

            #endregion

            #region User
            model.Entity<User>()
                .Property(p => p.Username)
                .IsRequired();

            model.Entity<User>()
              .Property(p => p.Password)
              .IsRequired();

            model.Entity<User>()
              .Property(p => p.Name)
              .IsRequired();

            model.Entity<User>()
              .Property(p => p.LastName)
              .IsRequired();

            model.Entity<User>()
              .Property(p => p.Email)
              .IsRequired();

            model.Entity<User>()
              .Property(p => p.Username)
              .IsRequired(false);
            #endregion

            #endregion
        }
    }
}
