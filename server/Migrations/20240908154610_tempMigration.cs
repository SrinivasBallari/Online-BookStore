using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class tempMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CartItems__book_id__12345678",
                table: "CartIterms");

            migrationBuilder.DropForeignKey(
                name: "FK__CartItems__cart_id__12345678",
                table: "CartIterms");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Books_BookId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK__Carts__user_id__5441852A",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Carts__2EF52A27B9CE3939",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_BookId",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CartItems__12345678",
                table: "CartIterms");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Books");

            migrationBuilder.RenameTable(
                name: "CartIterms",
                newName: "CartItems");

            migrationBuilder.RenameIndex(
                name: "IX_CartIterms_cart_id",
                table: "CartItems",
                newName: "IX_CartItems_cart_id");

            migrationBuilder.RenameIndex(
                name: "IX_CartIterms_book_id",
                table: "CartItems",
                newName: "IX_CartItems_book_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Carts__2EF52A274AC28DB8",
                table: "Carts",
                column: "cart_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__CartItem__5D9A6C6EE5EB3BC3",
                table: "CartItems",
                column: "cart_item_id");

            migrationBuilder.AddForeignKey(
                name: "FK__CartItems__book___656C112C",
                table: "CartItems",
                column: "book_id",
                principalTable: "Books",
                principalColumn: "book_id");

            migrationBuilder.AddForeignKey(
                name: "FK__CartItems__cart___6477ECF3",
                table: "CartItems",
                column: "cart_id",
                principalTable: "Carts",
                principalColumn: "cart_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Carts__user_id__619B8048",
                table: "Carts",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CartItems__book___656C112C",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK__CartItems__cart___6477ECF3",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK__Carts__user_id__619B8048",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Carts__2EF52A274AC28DB8",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK__CartItem__5D9A6C6EE5EB3BC3",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartIterms");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_cart_id",
                table: "CartIterms",
                newName: "IX_CartIterms_cart_id");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_book_id",
                table: "CartIterms",
                newName: "IX_CartIterms_book_id");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Carts__2EF52A27B9CE3939",
                table: "Carts",
                column: "cart_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK__CartItems__12345678",
                table: "CartIterms",
                column: "cart_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_BookId",
                table: "Carts",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK__CartItems__book_id__12345678",
                table: "CartIterms",
                column: "book_id",
                principalTable: "Books",
                principalColumn: "book_id");

            migrationBuilder.AddForeignKey(
                name: "FK__CartItems__cart_id__12345678",
                table: "CartIterms",
                column: "cart_id",
                principalTable: "Carts",
                principalColumn: "cart_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Books_BookId",
                table: "Carts",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "book_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Carts__user_id__5441852A",
                table: "Carts",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id");
        }
    }
}
