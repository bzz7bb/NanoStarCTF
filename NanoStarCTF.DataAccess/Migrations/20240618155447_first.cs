using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable
#pragma warning disable
namespace NanoStarCTF.DataAccess.Migrations;

/// <inheritdoc />
public partial class first : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Address",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Country = table.Column<string>(type: "text", nullable: false),
                Region = table.Column<string>(type: "text", nullable: false),
                District = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                AddressLine = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                State = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Address", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Attachments",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Path = table.Column<string>(type: "text", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                State = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Attachments", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Groups",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                Bio = table.Column<string>(type: "text", nullable: false),
                ImageId = table.Column<long>(type: "bigint", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                State = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Groups", x => x.Id);
                table.ForeignKey(
                    name: "FK_Groups_Attachments_ImageId",
                    column: x => x.ImageId,
                    principalTable: "Attachments",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Password = table.Column<string>(type: "text", nullable: false),
                Email = table.Column<string>(type: "text", nullable: false),
                AddressId = table.Column<long>(type: "bigint", nullable: true),
                Bio = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Gender = table.Column<int>(type: "integer", nullable: false),
                ImageId = table.Column<long>(type: "bigint", nullable: true),
                Role = table.Column<int>(type: "integer", nullable: false),
                DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                Points = table.Column<long>(type: "bigint", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                State = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_Users_Address_AddressId",
                    column: x => x.AddressId,
                    principalTable: "Address",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Users_Attachments_ImageId",
                    column: x => x.ImageId,
                    principalTable: "Attachments",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "CTFs",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                Description = table.Column<string>(type: "text", nullable: false),
                Link = table.Column<string>(type: "text", nullable: false),
                ImageId = table.Column<long>(type: "bigint", nullable: true),
                GroupId = table.Column<long>(type: "bigint", nullable: false),
                FlagChek = table.Column<string>(type: "text", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                State = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CTFs", x => x.Id);
                table.ForeignKey(
                    name: "FK_CTFs_Attachments_ImageId",
                    column: x => x.ImageId,
                    principalTable: "Attachments",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_CTFs_Groups_GroupId",
                    column: x => x.GroupId,
                    principalTable: "Groups",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "CTFUser",
            columns: table => new
            {
                AcceptedProblemsId = table.Column<long>(type: "bigint", nullable: false),
                UsersId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CTFUser", x => new { x.AcceptedProblemsId, x.UsersId });
                table.ForeignKey(
                    name: "FK_CTFUser_CTFs_AcceptedProblemsId",
                    column: x => x.AcceptedProblemsId,
                    principalTable: "CTFs",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CTFUser_Users_UsersId",
                    column: x => x.UsersId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Hints",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Discription = table.Column<string>(type: "text", nullable: false),
                Links = table.Column<string>(type: "text", nullable: true),
                CTFId = table.Column<long>(type: "bigint", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedBy = table.Column<long>(type: "bigint", nullable: true),
                CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                State = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Hints", x => x.Id);
                table.ForeignKey(
                    name: "FK_Hints_CTFs_CTFId",
                    column: x => x.CTFId,
                    principalTable: "CTFs",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_CTFs_GroupId",
            table: "CTFs",
            column: "GroupId");

        migrationBuilder.CreateIndex(
            name: "IX_CTFs_ImageId",
            table: "CTFs",
            column: "ImageId");

        migrationBuilder.CreateIndex(
            name: "IX_CTFUser_UsersId",
            table: "CTFUser",
            column: "UsersId");

        migrationBuilder.CreateIndex(
            name: "IX_Groups_ImageId",
            table: "Groups",
            column: "ImageId");

        migrationBuilder.CreateIndex(
            name: "IX_Hints_CTFId",
            table: "Hints",
            column: "CTFId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_AddressId",
            table: "Users",
            column: "AddressId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_ImageId",
            table: "Users",
            column: "ImageId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CTFUser");

        migrationBuilder.DropTable(
            name: "Hints");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "CTFs");

        migrationBuilder.DropTable(
            name: "Address");

        migrationBuilder.DropTable(
            name: "Groups");

        migrationBuilder.DropTable(
            name: "Attachments");
    }
}
