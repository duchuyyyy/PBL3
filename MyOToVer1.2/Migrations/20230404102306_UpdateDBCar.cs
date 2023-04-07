using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOToVer1._2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "car_name_img",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "car_name_img",
                table: "Cars");
        }
    }
}
