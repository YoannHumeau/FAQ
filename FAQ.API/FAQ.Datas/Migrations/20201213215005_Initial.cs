using Microsoft.EntityFrameworkCore.Migrations;

namespace FAQ.Datas.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    ContentText = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionModelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionModelId",
                        column: x => x.QuestionModelId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsTranslates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Language = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    QuestionText = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionModelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsTranslates", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_QuestionTranslateModel_QuestionModel",
                        column: x => x.QuestionModelId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionModelId",
                table: "Answers",
                column: "QuestionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsTranslates_Language_QuestionModelId",
                table: "QuestionsTranslates",
                columns: new[] { "Language", "QuestionModelId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsTranslates_QuestionModelId",
                table: "QuestionsTranslates",
                column: "QuestionModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "QuestionsTranslates");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
