using FAQ.Datas.Models;
using Microsoft.EntityFrameworkCore;

namespace FAQ.Datas.DataAccess
{
    internal class FAQContext : DbContext
    {
        private readonly string _connectionString;

        // Default constructor(used for creating migrations)
        // Have to be commented to secure production
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
        internal DbSet<QuestionTranslateModel> QuestionsTranslates { get; set; }
        internal DbSet<AnswerModel> Answers { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Question Translates
            modelBuilder.Entity<QuestionTranslateModel>()
                .HasIndex(qt => new { qt.Language, qt.QuestionModelId })
                .IsUnique();

            modelBuilder.Entity<QuestionTranslateModel>()
                .HasOne<QuestionModel>()
                .WithMany(q => q.QuestionTranslates)
                .HasForeignKey("QuestionModelId")
                .HasConstraintName("ForeignKey_QuestionTranslateModel_QuestionModel");

            // Answers
            modelBuilder.Entity<AnswerModel>()
                .HasIndex(a => new { a.Language, a.QuestionModelId })
                .IsUnique();

            modelBuilder.Entity<AnswerModel>()
                .HasOne<QuestionModel>()
                .WithMany(a => a.Answers)
                .HasForeignKey("QuestionModelId")
                .HasConstraintName("ForeignKey_QuestionTranslateModel_AnswerModel");
        }
    }
}
