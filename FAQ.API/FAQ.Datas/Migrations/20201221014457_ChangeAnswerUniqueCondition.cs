using Microsoft.EntityFrameworkCore.Migrations;

namespace FAQ.Datas.Migrations
{
    public partial class ChangeAnswerUniqueCondition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Answers_Language_QuestionModelId",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Language_QuestionModelId_Text",
                table: "Answers",
                columns: new[] { "Language", "QuestionModelId", "Text" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Answers_Language_QuestionModelId_Text",
                table: "Answers");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Language_QuestionModelId",
                table: "Answers",
                columns: new[] { "Language", "QuestionModelId" },
                unique: true);
        }
    }
}
