using FAQ.Datas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            // Add the shadow property to the model
            //modelBuilder.Entity<Post>()
            //    .Property<int>("BlogForeignKey");
            // Use the shadow property as a foreign key

            modelBuilder.Entity<QuestionTranslateModel>()
                .HasIndex(qt => new { qt.Language, qt.QuestionModelId })
                .IsUnique();

            modelBuilder.Entity<QuestionTranslateModel>()
                .HasOne<QuestionModel>()
                .WithMany(b => b.QuestionTranslates)
                .HasForeignKey("QuestionModelId")
                .HasConstraintName("ForeignKey_QuestionTranslateModel_QuestionModel");

            



            //////// Question
            ////////modelBuilder.Entity<QuestionModel>()
            ////////.HasMany(q => q.Answers);

            //////modelBuilder.Entity<QuestionModel>()
            //////    .HasMany(c => c.QuestionTranslates)
            //////    .WithOne(e => e.QuestionModel)
            //////    .IsRequired()
            //////    .OnDelete(DeleteBehavior.NoAction);

            //////////////modelBuilder.Entity<QuestionModel>()
            //////////////    .HasMany(q => q.QuestionTranslates)
            //////////////    .WithOne(qt => qt.QuestionModel);

            //////modelBuilder.Entity<QuestionTranslateModel>()
            //////    .HasOne(qt => qt.QuestionModel)
            //////    .WithMany(qt => qt.QuestionTranslates)
            //////    .HasForeignKey(qt => qt.QuestionModelId)
            //////    .IsRequired()
            //////    .OnDelete(DeleteBehavior.NoAction);

            //////modelBuilder.Entity<QuestionTranslateModel>()
            //////    .HasIndex(qt => new { qt.Language, qt.QuestionModelId })
            //////    .IsUnique();








            //modelBuilder.Entity<QuestionModel>()
            //    .HasOne(q => q.QuestionContentModel)
            //    .WithOne(qc => qc.QuestionModel)
            //    ;
            //modelBuilder.Entity<QuestionModel>()
            //.HasOne(q => q.QuestionContentModel)
            //.WithOne(qc => qc.QuestionModel)
            //.HasForeignKey<QuestionContentModel>(qc => qc.QuestionModelId)
            //;

            //modelBuilder.Entity<QuestionContentModel>()
            //    .HasOne(qc => qc.QuestionModel)
            //    ;

            //modelBuilder.Entity<QuestionTranslateModel>()
            //    .HasAlternateKey(c => new { c.Language, c.QuestionModelId })
            //    .HasName("IX_MultipleColumns");

            //modelBuilder.Entity<QuestionContentModel>()
            //    .HasOne(q => q.QuestionModel)
            //    .WithOne(q => q.Text)
            //    .IsRequired();


            // Answer
            //modelBuilder.Entity<AnswerModel>()
            //    .HasOne(a => a.Question)
            //    .WithMany(q => q.Answers)
            //    .HasForeignKey(s => s.QuestionModelId);
        }
    }
}
