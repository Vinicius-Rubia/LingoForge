using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LingoForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableAnswerAndAnswerItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChosenAlternativeId",
                table: "answer_items",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChosenAlternativeId",
                table: "answer_items");
        }
    }
}
