using FAQ.Datas.Models;
using Microsoft.EntityFrameworkCore;

namespace FAQ.Datas.DataAccess
{
    internal class FAQContext : DbContext
    {
        private readonly string _connectionString;

        // Default constructor(used for creating migrations)
        // Commented to secure production
        public FAQContext()
        {
            _connectionString = "FAQ.db";
        }

        internal FAQContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={_connectionString}");

        #region DbSets
        internal DbSet<QuestionModel> Questions { get; set; }
        internal DbSet<AnswerModel> Answers { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Question
            modelBuilder.Entity<QuestionModel>()
                .HasMany(q => q.Answers);

            // Answer
            modelBuilder.Entity<AnswerModel>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(s => s.QuestionModelId);
        }
    }
}
