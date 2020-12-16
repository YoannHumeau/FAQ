using Microsoft.EntityFrameworkCore.Migrations;

namespace FAQ.Datas.Migrations
{
    public partial class AddAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionModelId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "ContentText",
                table: "Answers",
                newName: "Text");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Language_QuestionModelId",
                table: "Answers",
                columns: new[] { "Language", "QuestionModelId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_QuestionTranslateModel_AnswerModel",
                table: "Answers",
                column: "QuestionModelId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_QuestionTranslateModel_AnswerModel",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_Language_QuestionModelId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Answers",
                newName: "ContentText");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionModelId",
                table: "Answers",
                column: "QuestionModelId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
