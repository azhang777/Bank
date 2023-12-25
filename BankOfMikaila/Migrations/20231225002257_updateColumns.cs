using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankOfMikaila.Migrations
{
    /// <inheritdoc />
    public partial class updateColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_Account1_Id",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_Account2_Id",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Account2_Id",
                table: "Transactions",
                newName: "ReceiverId");

            migrationBuilder.RenameColumn(
                name: "Account1_Id",
                table: "Transactions",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_Account2_Id",
                table: "Transactions",
                newName: "IX_Transactions_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_Account1_Id",
                table: "Transactions",
                newName: "IX_Transactions_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_OwnerId",
                table: "Transactions",
                column: "OwnerId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_ReceiverId",
                table: "Transactions",
                column: "ReceiverId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_OwnerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_ReceiverId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Transactions",
                newName: "Account2_Id");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Transactions",
                newName: "Account1_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_ReceiverId",
                table: "Transactions",
                newName: "IX_Transactions_Account2_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OwnerId",
                table: "Transactions",
                newName: "IX_Transactions_Account1_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_Account1_Id",
                table: "Transactions",
                column: "Account1_Id",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_Account2_Id",
                table: "Transactions",
                column: "Account2_Id",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
