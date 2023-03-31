using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOToVer1._2.Migrations
{
    /// <inheritdoc />
    public partial class updateCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "car_number_rented",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "car_number_rented",
                table: "Cars");
        }
    }
}
