﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAdvert.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertDBModel",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertDBModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertDBModel");
        }
    }
}
