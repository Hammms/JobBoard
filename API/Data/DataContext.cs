using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int,
     IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
     IdentityRoleClaim<int>, IdentityUserToken<int>>
     
    {   //Constructor, adds to startup and dependency injection container
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        //Create a database with the AppUser class and names it Users in the database
        public DbSet<UserSave> Save { get; set; }
        public DbSet<Message>  Messages { get; set; }
        public DbSet<Resume> Resume { get; set;}
        public DbSet<JobListings> Listings { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<UserSave>()
                .HasKey(k => new {k.SourceUserId, k.TargetUserId});

            builder.Entity<UserSave>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.SavedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);
            
             builder.Entity<UserSave>()
                .HasOne(s => s.TargetUser)
                .WithMany(l => l.SavedByUsers)
                .HasForeignKey(s => s.TargetUserId)
                .OnDelete(DeleteBehavior.Cascade);
                // .OnDelete(DeleteBehavior.NoAction);
                // If you are using SQLServer you are going to have to change one of these to no action
                // it will attempt a double delete and the second will try to delete something to doesnt exsist or even worse delete a user that didnt ask 

            builder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);
                
        }
    }
}

