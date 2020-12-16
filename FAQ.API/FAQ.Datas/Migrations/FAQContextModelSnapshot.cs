﻿// <auto-generated />
using FAQ.Datas.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FAQ.Datas.Migrations
{
    [DbContext(typeof(FAQContext))]
    partial class FAQContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("FAQ.Datas.Models.AnswerModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<int>("QuestionModelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionModelId");

                    b.HasIndex("Language", "QuestionModelId")
                        .IsUnique();

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("FAQ.Datas.Models.QuestionModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("FAQ.Datas.Models.QuestionTranslateModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<int>("QuestionModelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("QuestionText")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionModelId");

                    b.HasIndex("Language", "QuestionModelId")
                        .IsUnique();

                    b.ToTable("QuestionsTranslates");
                });

            modelBuilder.Entity("FAQ.Datas.Models.AnswerModel", b =>
                {
                    b.HasOne("FAQ.Datas.Models.QuestionModel", null)
                        .WithMany("Answers")
                        .HasForeignKey("QuestionModelId")
                        .HasConstraintName("ForeignKey_QuestionTranslateModel_AnswerModel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FAQ.Datas.Models.QuestionTranslateModel", b =>
                {
                    b.HasOne("FAQ.Datas.Models.QuestionModel", null)
                        .WithMany("QuestionTranslates")
                        .HasForeignKey("QuestionModelId")
                        .HasConstraintName("ForeignKey_QuestionTranslateModel_QuestionModel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FAQ.Datas.Models.QuestionModel", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("QuestionTranslates");
                });
#pragma warning restore 612, 618
        }
    }
}
