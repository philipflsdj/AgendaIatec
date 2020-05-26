using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.App.Data.Migrations
{
    public partial class Agendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_AspNetUsers_UsuarioId1",
                table: "Agendas");

            migrationBuilder.DropIndex(
                name: "IX_Agendas_UsuarioId1",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Agendas");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Agendas",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_UsuarioId",
                table: "Agendas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_AspNetUsers_UsuarioId",
                table: "Agendas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_AspNetUsers_UsuarioId",
                table: "Agendas");

            migrationBuilder.DropIndex(
                name: "IX_Agendas_UsuarioId",
                table: "Agendas");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Agendas",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Agendas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_UsuarioId1",
                table: "Agendas",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_AspNetUsers_UsuarioId1",
                table: "Agendas",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
