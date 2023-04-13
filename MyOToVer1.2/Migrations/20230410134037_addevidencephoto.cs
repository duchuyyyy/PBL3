using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyOToVer1._2.Migrations
{
    /// <inheritdoc />
    public partial class addevidencephoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img_confirm_transfer",
                table: "CarRentals");

            migrationBuilder.CreateTable(
                name: "TransferEvidencePhotos",
                columns: table => new
                {
                    rental_id = table.Column<int>(type: "int", nullable: false),
                    name_img = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferEvidencePhotos", x => x.rental_id);
                    table.ForeignKey(
                        name: "FK_TransferEvidencePhotos_CarRentals_rental_id",
                        column: x => x.rental_id,
                        principalTable: "CarRentals",
                        principalColumn: "rental_id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferEvidencePhotos");

            migrationBuilder.AddColumn<string>(
                name: "img_confirm_transfer",
                table: "CarRentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
