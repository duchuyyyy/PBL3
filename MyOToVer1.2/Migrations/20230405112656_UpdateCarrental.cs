using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOToVer1._2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCarrental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deposit_method",
                table: "CarRentals",
                newName: "img_confirm_transfer");

            migrationBuilder.AddColumn<string>(
                name: "owner_name_banking",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "owner_number_account",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "rental_status",
                table: "CarRentals",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "deposit_status",
                table: "CarRentals",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "owner_name_banking",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "owner_number_account",
                table: "Owners");

            migrationBuilder.RenameColumn(
                name: "img_confirm_transfer",
                table: "CarRentals",
                newName: "deposit_method");

            migrationBuilder.AlterColumn<bool>(
                name: "rental_status",
                table: "CarRentals",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "deposit_status",
                table: "CarRentals",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
