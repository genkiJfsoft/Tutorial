using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Core.Providers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseUserRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Expense",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionBy",
                table: "Expense",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Expense",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_CreatedBy",
                table: "Expense",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_TransactionBy",
                table: "Expense",
                column: "TransactionBy");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_UpdatedBy",
                table: "Expense",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_User_CreatedBy",
                table: "Expense",
                column: "CreatedBy",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_User_TransactionBy",
                table: "Expense",
                column: "TransactionBy",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_User_UpdatedBy",
                table: "Expense",
                column: "UpdatedBy",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_User_CreatedBy",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_User_TransactionBy",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_User_UpdatedBy",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Expense_CreatedBy",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Expense_TransactionBy",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Expense_UpdatedBy",
                table: "Expense");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "Expense",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransactionBy",
                table: "Expense",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Expense",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
