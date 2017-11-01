using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace asp_ecommerce.Migrations
{
    public partial class changed_product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserEmail",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserName",
                table: "Products",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationUserName",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserEmail",
                table: "Products",
                nullable: true);
        }
    }
}
