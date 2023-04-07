using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOToVer1._2.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFieldOwnernumberrented : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "owner_number_rented",
                table: "Owners");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "owner_number_rented",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
