using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    author_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    bio = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Authors__86516BCFA24EBAF7", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    payment_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    amount = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__ED1FC9EA0B9BA1AD", x => x.payment_id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    publisher_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    publisher_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    publisher_address = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Publishe__3263F29DAFF3A768", x => x.publisher_id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tag = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tags__4296A2B663E5BFA0", x => x.tag_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    contact = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    pin_code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B9BE370F0B2D23E4", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    author_id = table.Column<int>(type: "int", nullable: true),
                    pages_count = table.Column<int>(type: "int", nullable: true),
                    language = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    publisher_id = table.Column<int>(type: "int", nullable: true),
                    published_date = table.Column<DateOnly>(type: "date", nullable: true),
                    published_version = table.Column<double>(type: "float", nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Books__490D1AE1A84A3ED0", x => x.book_id);
                    table.ForeignKey(
                        name: "FK__Books__author_id__3B75D760",
                        column: x => x.author_id,
                        principalTable: "Authors",
                        principalColumn: "author_id");
                    table.ForeignKey(
                        name: "FK__Books__publisher__3C69FB99",
                        column: x => x.publisher_id,
                        principalTable: "Publishers",
                        principalColumn: "publisher_id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    payment_id = table.Column<int>(type: "int", nullable: true),
                    order_date = table.Column<DateOnly>(type: "date", nullable: true),
                    total = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__465962298B4F3117", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__Orders__payment___4BAC3F29",
                        column: x => x.payment_id,
                        principalTable: "Payments",
                        principalColumn: "payment_id");
                    table.ForeignKey(
                        name: "FK__Orders__user_id__4AB81AF0",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "BookTag",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false),
                    tag_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookTag__3D2470CACC5A9021", x => new { x.book_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK__BookTag__book_id__440B1D61",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK__BookTag__tag_id__44FF419A",
                        column: x => x.tag_id,
                        principalTable: "Tags",
                        principalColumn: "tag_id");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    cart_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Carts__2EF52A27B9CE3939", x => x.cart_id);
                    table.ForeignKey(
                        name: "FK__Carts__book_id__534D60F1",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK__Carts__user_id__5441852A",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    review = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    reviewed_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__60883D90FB2C6292", x => x.review_id);
                    table.ForeignKey(
                        name: "FK__Reviews__book_id__4E88ABD4",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK__Reviews__user_id__4F7CD00D",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Order_items",
                columns: table => new
                {
                    order_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    book_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order_it__3764B6BC850F45D3", x => x.order_item_id);
                    table.ForeignKey(
                        name: "FK__Order_ite__book___5812160E",
                        column: x => x.book_id,
                        principalTable: "Books",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "FK__Order_ite__order__571DF1D5",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_author_id",
                table: "Books",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_Books_publisher_id",
                table: "Books",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_BookTag_tag_id",
                table: "BookTag",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_book_id",
                table: "Carts",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_user_id",
                table: "Carts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_items_book_id",
                table: "Order_items",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_items_order_id",
                table: "Order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_payment_id",
                table: "Orders",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_user_id",
                table: "Orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_book_id",
                table: "Reviews",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_user_id",
                table: "Reviews",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTag");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Order_items");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
