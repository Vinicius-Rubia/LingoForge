using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeAnswerItemColumnsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AnswerText",
                table: "answer_items",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_answer_items_ChosenAlternativeId",
                table: "answer_items",
                column: "ChosenAlternativeId");

            migrationBuilder.AddForeignKey(
                name: "FK_answer_items_alternatives_ChosenAlternativeId",
                table: "answer_items",
                column: "ChosenAlternativeId",
                principalTable: "alternatives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_answer_items_alternatives_ChosenAlternativeId",
                table: "answer_items");

            migrationBuilder.DropIndex(
                name: "IX_answer_items_ChosenAlternativeId",
                table: "answer_items");

            migrationBuilder.AlterColumn<string>(
                name: "AnswerText",
                table: "answer_items",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
