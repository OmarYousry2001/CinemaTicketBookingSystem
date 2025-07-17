using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaTicketBookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSecurityFieldsToMovieActorAndMovieGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "MovieGenres",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateUtc",
                table: "MovieGenres",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "MovieGenres",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "MovieGenres",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "MovieGenres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateUtc",
                table: "MovieGenres",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "MovieActors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateUtc",
                table: "MovieActors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "MovieActors",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "MovieActors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "MovieActors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateUtc",
                table: "MovieActors",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "CreatedDateUtc",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "UpdatedDateUtc",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MovieActors");

            migrationBuilder.DropColumn(
                name: "CreatedDateUtc",
                table: "MovieActors");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "MovieActors");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieActors");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MovieActors");

            migrationBuilder.DropColumn(
                name: "UpdatedDateUtc",
                table: "MovieActors");
        }
    }
}
