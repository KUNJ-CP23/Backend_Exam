using dotnet_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace dotnet_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) {}
        
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Ticket_Comments> Comments { get; set; }
        public DbSet<Ticket_Status_Logs> Ticket_Status_Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.email)
                .IsUnique();

            //one to many, 2 vaar users use karva mate 

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.created_by)
                .OnDelete(DeleteBehavior.Restrict);
            
            //aaya deleete nathi thava devana atle restrict
            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.AssignedTo)
                .WithMany()
                .HasForeignKey(t => t.assigned_to)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket_Comments>()
                .HasOne(tc=>tc.Tickets)
                .WithMany(t=>t.Ticket_Comments)
                .HasForeignKey(tc=>tc.ticket_id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
