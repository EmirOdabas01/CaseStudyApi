using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseStudyApi.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImageFiles_ProductColors_ProductColorId",
                table: "ProductImageFiles");

            migrationBuilder.AlterColumn<int>(
                name: "ProductColorId",
                table: "ProductImageFiles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImageFiles_ProductColors_ProductColorId",
                table: "ProductImageFiles",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImageFiles_ProductColors_ProductColorId",
                table: "ProductImageFiles");

            migrationBuilder.AlterColumn<int>(
                name: "ProductColorId",
                table: "ProductImageFiles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImageFiles_ProductColors_ProductColorId",
                table: "ProductImageFiles",
                column: "ProductColorId",
                principalTable: "ProductColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
