using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cep_furniture_store.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "categories",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        image = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        status = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_categories", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "orders",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        OrderDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        status = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_orders", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "products",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        price = table.Column<double>(type: "float", nullable: false),
            //        material = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        color = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        dimention = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        quantity = table.Column<int>(type: "int", nullable: false),
            //        status = table.Column<int>(type: "int", nullable: false),
            //        categoryId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_products", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "roles",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        status = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_roles", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "subCategories",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        status = table.Column<int>(type: "int", nullable: false),
            //        categoryId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_subCategories", x => x.id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "users",
            //    columns: table => new
            //    {
            //        id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        email = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        password = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        status = table.Column<int>(type: "int", nullable: false),
            //        roleId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_users", x => x.id);
            //        table.ForeignKey(
            //            name: "FK_users_roles_roleId",
            //            column: x => x.roleId,
            //            principalTable: "roles",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_users_roleId",
            //    table: "users",
            //    column: "roleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "categories");

            //migrationBuilder.DropTable(
            //    name: "orders");

            //migrationBuilder.DropTable(
            //    name: "products");

            //migrationBuilder.DropTable(
            //    name: "subCategories");

            //migrationBuilder.DropTable(
            //    name: "users");

            //migrationBuilder.DropTable(
            //    name: "roles");
        }
    }
}
