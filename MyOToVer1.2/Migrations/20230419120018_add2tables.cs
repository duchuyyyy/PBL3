using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOToVer1._2.Migrations
{
    /// <inheritdoc />
    public partial class add2tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Customers",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Cars",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "CarRentals",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerIdentityPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameImg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerIdentityPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerIdentityPhotos_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AdminId",
                table: "Customers",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_AdminId",
                table: "Cars",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRentals_AdminId",
                table: "CarRentals",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerIdentityPhotos_OwnerId",
                table: "OwnerIdentityPhotos",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRentals_Admins_AdminId",
                table: "CarRentals",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Admins_AdminId",
                table: "Cars",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Admins_AdminId",
                table: "Customers",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRentals_Admins_AdminId",
                table: "CarRentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Admins_AdminId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Admins_AdminId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "OwnerIdentityPhotos");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AdminId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Cars_AdminId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_CarRentals_AdminId",
                table: "CarRentals");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "CarRentals");
        }
    }
}
