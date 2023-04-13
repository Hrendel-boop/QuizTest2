using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizTest.Migrations
{
    /// <inheritdoc />
    public partial class rework2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Test_Question_QuestionId",
                table: "Test");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Test_QuestionId",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Test");

            migrationBuilder.RenameColumn(
                name: "QuestionsId",
                table: "Test",
                newName: "QuestionsSerialized");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuestionsSerialized",
                table: "Test",
                newName: "QuestionsId");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Test",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncorrectAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Test_QuestionId",
                table: "Test",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Question_QuestionId",
                table: "Test",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
