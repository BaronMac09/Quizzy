using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quizzy.Models;

namespace Quizzy.Data
{
    public class QuizzyContext : DbContext
    {
        public QuizzyContext (DbContextOptions<QuizzyContext> options)
            : base(options)
        {
        }
        
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Attempt> Attempts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Quiz>().ToTable("Quiz");
            modelBuilder.Entity<Assignment>().ToTable("Assignment");
            modelBuilder.Entity<Attempt>().ToTable("Attempt");
        }
    }
}
