using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace asp_ecommerce.Migrations
{
    public partial class refactored2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //name: "FK_Customers_AspNetUsers_UserId",
            //table: "Customers");

            //migrationBuilder.DropForeignKey(
            //name: "FK_Products_AspNetUsers_UserId",
            //table: "Products");

            //migrationBuilder.DropIndex(
            //name: "IX_Products_UserId",
            //table: "Products");

            //migrationBuilder.DropIndex(
            //name: "IX_Customers_UserId",
            //table: "Customers");

            //migrationBuilder.DropColumn(
            //name: "ApplicationUserId",
            //table: "Products");

            //migrationBuilder.DropColumn(
            //name: "ApplicationUserId",
            //table: "Customers");

            //migrationBuilder.AddColumn<int>(
                //name: "UserId",
                //table: "Products",
                //type: "int",
                //nullable: false);

            //migrationBuilder.AddColumn<string>(
                //name: "UserId1",
                //table: "Products",
                //type: "varchar(127)",
                //nullable: true);

            //migrationBuilder.AddColumn<int>(
            //   name: "UserId",
            //   table: "Customers",
            //   type: "int",
            //   nullable: false);

            //migrationBuilder.AlterColumn<int>(
                //name: "UserId",
                //table: "Customers",
                //type: "int",
                //nullable: false,
                //oldClrType: typeof(string),
                //oldNullable: true);

            //migrationBuilder.CreateTable(
                //name: "AspNetRoles",
                //columns: table => new
                //{
                //    Id = table.Column<string>(type: "varchar(127)", nullable: false),
                //    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                //    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                //    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                //},
                //constraints: table =>
                //{
                //    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                //});

            //migrationBuilder.CreateTable(
                //name: "AspNetUserClaims",
                //columns: table => new
                //{
                //    Id = table.Column<int>(type: "int", nullable: false)
                //        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                //    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                //    ClaimValue = table.Column<string>(type: "longtext", nullable: true),
                //    UserId = table.Column<string>(type: "varchar(127)", nullable: false)
                //},
                //constraints: table =>
                //{
                //    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                //    table.ForeignKey(
                //        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                //        column: x => x.UserId,
                //        principalTable: "AspNetUsers",
                //        principalColumn: "Id",
                //        onDelete: ReferentialAction.Cascade);
                //});

            //migrationBuilder.CreateTable(
                //name: "AspNetUserLogins",
                //columns: table => new
                //{
                //    LoginProvider = table.Column<string>(type: "varchar(127)", nullable: false),
                //    ProviderKey = table.Column<string>(type: "varchar(127)", nullable: false),
                //    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                //    UserId = table.Column<string>(type: "varchar(127)", nullable: false)
                //},
                //constraints: table =>
                //{
                //    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                //    table.ForeignKey(
                //        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                //        column: x => x.UserId,
                //        principalTable: "AspNetUsers",
                //        principalColumn: "Id",
                //        onDelete: ReferentialAction.Cascade);
                //});

            //migrationBuilder.CreateTable(
                //name: "AspNetUserTokens",
                //columns: table => new
                //{
                //    UserId = table.Column<string>(type: "varchar(127)", nullable: false),
                //    LoginProvider = table.Column<string>(type: "varchar(127)", nullable: false),
                //    Name = table.Column<string>(type: "varchar(127)", nullable: false),
                //    Value = table.Column<string>(type: "longtext", nullable: true)
                //},
                //constraints: table =>
                //{
                //    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                //    table.ForeignKey(
                //        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                //        column: x => x.UserId,
                //        principalTable: "AspNetUsers",
                //        principalColumn: "Id",
                //        onDelete: ReferentialAction.Cascade);
                //});

            //migrationBuilder.CreateTable(
                //name: "AspNetRoleClaims",
                //columns: table => new
                //{
                //    Id = table.Column<int>(type: "int", nullable: false)
                //        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                //    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                //    ClaimValue = table.Column<string>(type: "longtext", nullable: true),
                //    RoleId = table.Column<string>(type: "varchar(127)", nullable: false)
                //},
                //constraints: table =>
                //{
                //    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                //    table.ForeignKey(
                //        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                //        column: x => x.RoleId,
                //        principalTable: "AspNetRoles",
                //        principalColumn: "Id",
                //        onDelete: ReferentialAction.Cascade);
                //});

            //migrationBuilder.CreateTable(
                //name: "AspNetUserRoles",
                //columns: table => new
                //{
                //    UserId = table.Column<string>(type: "varchar(127)", nullable: false),
                //    RoleId = table.Column<string>(type: "varchar(127)", nullable: false)
                //},
                //constraints: table =>
                //{
                //    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                //    table.ForeignKey(
                //        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                //        column: x => x.RoleId,
                //        principalTable: "AspNetRoles",
                //        principalColumn: "Id",
                //        onDelete: ReferentialAction.Cascade);
                //    table.ForeignKey(
                //        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                //        column: x => x.UserId,
                //        principalTable: "AspNetUsers",
                //        principalColumn: "Id",
                //        onDelete: ReferentialAction.Cascade);
                //});

            //migrationBuilder.CreateIndex(
                //name: "IX_Products_UserId",
                //table: "Products",
                //column: "UserId");

            //migrationBuilder.CreateIndex(
                //name: "IX_Customers_UserId",
                //table: "Customers",
                //column: "UserId",
                //unique: false);

            //migrationBuilder.CreateIndex(
                //name: "IX_AspNetRoleClaims_RoleId",
                //table: "AspNetRoleClaims",
                //column: "RoleId");

            //migrationBuilder.CreateIndex(
                //name: "RoleNameIndex",
                //table: "AspNetRoles",
                //column: "NormalizedName",
                //unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserClaims_UserId",
            //    table: "AspNetUserClaims",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_AspNetUserLogins_UserId",
            //    table: "AspNetUserLogins",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
                //name: "IX_AspNetUserRoles_RoleId",
                //table: "AspNetUserRoles",
                //column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Customers_AspNetUsers_UserId",
            //    table: "Customers");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Products_AspNetUsers_UserId1",
            //    table: "Products");

            //migrationBuilder.DropTable(
            //    name: "AspNetRoleClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserClaims");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserLogins");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserRoles");

            //migrationBuilder.DropTable(
            //    name: "AspNetUserTokens");

            //migrationBuilder.DropTable(
            //    name: "AspNetRoles");

            //migrationBuilder.DropIndex(
            //    name: "IX_Products_UserId1",
            //    table: "Products");

            //migrationBuilder.DropIndex(
            //    name: "IX_Customers_UserId1",
            //    table: "Customers");

            //migrationBuilder.DropColumn(
            //    name: "UserId1",
            //    table: "Products");

            //migrationBuilder.DropColumn(
            //    name: "UserId1",
            //    table: "Customers");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "Products",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AddColumn<int>(
            //    name: "ApplicationUserId",
            //    table: "Products",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "Customers",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AddColumn<int>(
            //    name: "ApplicationUserId",
            //    table: "Customers",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Products_UserId",
            //    table: "Products",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Customers_UserId",
            //    table: "Customers",
            //    column: "UserId",
            //    unique: true);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Customers_AspNetUsers_UserId",
            //    table: "Customers",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
                //name: "FK_Products_AspNetUsers_UserId",
                //table: "Products",
                //column: "UserId",
                //principalTable: "AspNetUsers",
                //principalColumn: "Id",
                //onDelete: ReferentialAction.Restrict);
        }
    }
}
