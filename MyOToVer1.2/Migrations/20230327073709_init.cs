using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOToVer1._2.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    owner_revenue = table.Column<long>(type: "bigint", nullable: false),
                    owner_number_rented = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Owners_Customers_Id",
                        column: x => x.Id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    car_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_capacity = table.Column<int>(type: "int", nullable: false),
                    car_model_year = table.Column<int>(type: "int", nullable: false),
                    car_tranmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_fuel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_consume_fuel = table.Column<int>(type: "int", nullable: false),
                    car_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_price = table.Column<int>(type: "int", nullable: false),
                    car_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_rule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_status = table.Column<bool>(type: "bit", nullable: false),
                    owner_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.car_id);
                    table.ForeignKey(
                        name: "FK_Cars_Owners_owner_id",
                        column: x => x.owner_id,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Car_Imgs",
                columns: table => new
                {
                    img_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    img_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    img_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    car_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car_Imgs", x => x.img_id);
                    table.ForeignKey(
                        name: "FK_Car_Imgs_Cars_car_id",
                        column: x => x.car_id,
                        principalTable: "Cars",
                        principalColumn: "car_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarRentals",
                columns: table => new
                {
                    rental_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rental_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    return_datetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rental_status = table.Column<bool>(type: "bit", nullable: false),
                    deposit_method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deposit_status = table.Column<bool>(type: "bit", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    car_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRentals", x => x.rental_id);
                    table.ForeignKey(
                        name: "FK_CarRentals_Cars_car_id",
                        column: x => x.car_id,
                        principalTable: "Cars",
                        principalColumn: "car_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarRentals_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_Imgs_car_id",
                table: "Car_Imgs",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_car_id",
                table: "CarRentals",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_customer_id",
                table: "CarRentals",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_owner_id",
                table: "Cars",
                column: "owner_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car_Imgs");

            migrationBuilder.DropTable(
                name: "CarRentals");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
