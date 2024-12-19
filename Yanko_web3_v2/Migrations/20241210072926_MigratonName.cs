using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yanko_web3_v2.Migrations
{
    /// <inheritdoc />
    public partial class MigratonName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Tag_table",
                columns: table => new
                {
                    teg_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teg_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_1", x => x.teg_id);
                });

            migrationBuilder.CreateTable(
                name: "User_table",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_table", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_User_table_Role",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Channel_table",
                columns: table => new
                {
                    channel_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    channel_name = table.Column<string>(type: "text", nullable: false),
                    autor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel_table", x => x.channel_id);
                    table.ForeignKey(
                        name: "FK_Channel_table_User_table",
                        column: x => x.autor_id,
                        principalTable: "User_table",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Collection_table",
                columns: table => new
                {
                    collection_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    collection_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection_table", x => x.collection_id);
                    table.ForeignKey(
                        name: "FK_Collection_table_User_table",
                        column: x => x.user_id,
                        principalTable: "User_table",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions_table",
                columns: table => new
                {
                    subscriptions_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    channel_id = table.Column<int>(type: "int", nullable: false),
                    subscriptions_level = table.Column<int>(type: "int", nullable: false),
                    subscribers_count = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Subscriptions_table_User_table",
                        column: x => x.user_id,
                        principalTable: "User_table",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction_table",
                columns: table => new
                {
                    transaction_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recipient_id = table.Column<int>(type: "int", nullable: false),
                    sender_id = table.Column<int>(type: "int", nullable: false),
                    sum = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction_table", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_Transaction_table_User_table",
                        column: x => x.recipient_id,
                        principalTable: "User_table",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK_Transaction_table_User_table1",
                        column: x => x.sender_id,
                        principalTable: "User_table",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Object_table",
                columns: table => new
                {
                    Object_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Object_description = table.Column<string>(type: "text", nullable: true),
                    author_id = table.Column<int>(type: "int", nullable: false),
                    collection_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Object_table", x => x.Object_id);
                    table.ForeignKey(
                        name: "FK_Object_table_Collection_table",
                        column: x => x.collection_id,
                        principalTable: "Collection_table",
                        principalColumn: "collection_id");
                });

            migrationBuilder.CreateTable(
                name: "Advertisement_table",
                columns: table => new
                {
                    advertisement_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    object_id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<double>(type: "float", nullable: true),
                    sale = table.Column<int>(type: "int", nullable: true),
                    teg_id = table.Column<int>(type: "int", nullable: false),
                    channel_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement_table", x => x.advertisement_id);
                    table.ForeignKey(
                        name: "FK_Advertisement_table_Channel_table",
                        column: x => x.channel_id,
                        principalTable: "Channel_table",
                        principalColumn: "channel_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advertisement_table_Object_table",
                        column: x => x.object_id,
                        principalTable: "Object_table",
                        principalColumn: "Object_id");
                    table.ForeignKey(
                        name: "FK_Advertisement_table_Table_1",
                        column: x => x.teg_id,
                        principalTable: "Tag_table",
                        principalColumn: "teg_id");
                });

            migrationBuilder.CreateTable(
                name: "Comment_table",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    object_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    comment_text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment_text", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_Comment_table_Object_table",
                        column: x => x.object_id,
                        principalTable: "Object_table",
                        principalColumn: "Object_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image_table",
                columns: table => new
                {
                    image_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image = table.Column<byte[]>(type: "image", nullable: false),
                    object_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image_table", x => x.image_id);
                    table.ForeignKey(
                        name: "FK_Image_table_Object_table",
                        column: x => x.object_id,
                        principalTable: "Object_table",
                        principalColumn: "Object_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_table_channel_id",
                table: "Advertisement_table",
                column: "channel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_table_object_id",
                table: "Advertisement_table",
                column: "object_id");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_table_teg_id",
                table: "Advertisement_table",
                column: "teg_id");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_table_autor_id",
                table: "Channel_table",
                column: "autor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_table_user_id",
                table: "Collection_table",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_table_object_id",
                table: "Comment_table",
                column: "object_id");

            migrationBuilder.CreateIndex(
                name: "IX_Image_table_object_id",
                table: "Image_table",
                column: "object_id");

            migrationBuilder.CreateIndex(
                name: "IX_Object_table_collection_id",
                table: "Object_table",
                column: "collection_id");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_table_user_id",
                table: "Subscriptions_table",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_table_recipient_id",
                table: "Transaction_table",
                column: "recipient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_table_sender_id",
                table: "Transaction_table",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_table_role_id",
                table: "User_table",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisement_table");

            migrationBuilder.DropTable(
                name: "Comment_table");

            migrationBuilder.DropTable(
                name: "Image_table");

            migrationBuilder.DropTable(
                name: "Subscriptions_table");

            migrationBuilder.DropTable(
                name: "Transaction_table");

            migrationBuilder.DropTable(
                name: "Channel_table");

            migrationBuilder.DropTable(
                name: "Tag_table");

            migrationBuilder.DropTable(
                name: "Object_table");

            migrationBuilder.DropTable(
                name: "Collection_table");

            migrationBuilder.DropTable(
                name: "User_table");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
