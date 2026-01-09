using Domain.Entities;
using Domain.Entities.Middlewares;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infastructure.Context
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserSubmissions)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Problem>()
                .HasMany(sub => sub.ProblemSubmissions)
                .WithOne(sub => sub.Problem)
                .HasForeignKey(sub => sub.ProblemId);
        }

        public DbSet<User> UserEntity { get; set; } = null!;
        public DbSet<Submission> SubmissionEntity { get; set; } = null!;
        public DbSet<Problem> ProblemEntity { get; set; } = null!;
        public DbSet<IdempotencyRecord> IdempotencyEntity { get; set; } = null!;
    }
}
