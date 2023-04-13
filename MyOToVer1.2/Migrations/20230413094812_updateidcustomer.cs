using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOToVer1._2.Migrations
{
    /// <inheritdoc />
    public partial class updateidcustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReviews_Customers_Id",
                table: "CarReviews");

            migrationBuilder.DropIndex(
                name: "IX_CarReviews_Id",
                table: "CarReviews");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CarReviews");

            migrationBuilder.CreateIndex(
                name: "IX_CarReviews_CustomerID",
                table: "CarReviews",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReviews_Customers_CustomerID",
                table: "CarReviews",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarReviews_Customers_CustomerID",
                table: "CarReviews");

            migrationBuilder.DropIndex(
                name: "IX_CarReviews_CustomerID",
                table: "CarReviews");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CarReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarReviews_Id",
                table: "CarReviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarReviews_Customers_Id",
                table: "CarReviews",
                column: "Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
