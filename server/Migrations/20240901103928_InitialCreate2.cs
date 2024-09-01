using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__BookTag__book_id__440B1D61",
                table: "BookTag");

            migrationBuilder.DropForeignKey(
                name: "FK__BookTag__tag_id__44FF419A",
                table: "BookTag");

            migrationBuilder.AddForeignKey(
                name: "FK__BookTag__book_id__440B1D61",
                table: "BookTag",
                column: "book_id",
                principalTable: "Books",
                principalColumn: "book_id");

            migrationBuilder.AddForeignKey(
                name: "FK__BookTag__tag_id__44FF419A",
                table: "BookTag",
                column: "tag_id",
                principalTable: "Tags",
                principalColumn: "tag_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__BookTag__book_id__440B1D61",
                table: "BookTag");

            migrationBuilder.DropForeignKey(
                name: "FK__BookTag__tag_id__44FF419A",
                table: "BookTag");

            migrationBuilder.AddForeignKey(
                name: "FK__BookTag__book_id__440B1D61",
                table: "BookTag",
                column: "book_id",
                principalTable: "Books",
                principalColumn: "book_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__BookTag__tag_id__44FF419A",
                table: "BookTag",
                column: "tag_id",
                principalTable: "Tags",
                principalColumn: "tag_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
