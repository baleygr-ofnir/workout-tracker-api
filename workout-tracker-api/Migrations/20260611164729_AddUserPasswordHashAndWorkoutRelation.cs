using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workout_tracker_api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPasswordHashAndWorkoutRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Workouts",
                newName: "CreatorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Workouts",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_CreatorId",
                table: "Workouts",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_CreatorId",
                table: "Workouts",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Users_CreatorId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_CreatorId",
                table: "Workouts");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "Workouts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Workouts",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Users_UserId",
                table: "Workouts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}