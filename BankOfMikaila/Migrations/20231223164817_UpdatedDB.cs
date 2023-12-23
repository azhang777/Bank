using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankOfMikaila.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Accounts_Account2_Id",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_Account2_Id",
                table: "Transaction");

            migrationBuilder.AlterColumn<long>(
                name: "Account2_Id",
                table: "Transaction",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "Account2Id",
                table: "Transaction",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Transaction",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Account2Id",
                table: "Transaction",
                column: "Account2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Accounts_Account2Id",
                table: "Transaction",
                column: "Account2Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Accounts_Account2Id",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_Account2Id",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Account2Id",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Transaction");

            migrationBuilder.AlterColumn<long>(
                name: "Account2_Id",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_Account2_Id",
                table: "Transaction",
                column: "Account2_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Accounts_Account2_Id",
                table: "Transaction",
                column: "Account2_Id",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
