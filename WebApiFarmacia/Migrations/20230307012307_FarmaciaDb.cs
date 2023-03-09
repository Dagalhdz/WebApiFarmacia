using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiFarmacia.Migrations
{
    /// <inheritdoc />
    public partial class FarmaciaDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Laboratorio",
                columns: table => new
                {
                    LaboratorioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorio", x => x.LaboratorioId);
                });

            migrationBuilder.CreateTable(
                name: "ModoAdministracion",
                columns: table => new
                {
                    ModoAdministracionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModoAdministracion", x => x.ModoAdministracionId);
                });

            migrationBuilder.CreateTable(
                name: "Trabajador",
                columns: table => new
                {
                    TrabajadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajador", x => x.TrabajadorId);
                });

            migrationBuilder.CreateTable(
                name: "Medicamento",
                columns: table => new
                {
                    MedicamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LaboratorioId = table.Column<int>(type: "int", nullable: false),
                    ModoAdministracionId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    FechaCaducidad = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamento", x => x.MedicamentoId);
                    table.ForeignKey(
                        name: "FK_Medicamento_Laboratorio_LaboratorioId",
                        column: x => x.LaboratorioId,
                        principalTable: "Laboratorio",
                        principalColumn: "LaboratorioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medicamento_ModoAdministracion_ModoAdministracionId",
                        column: x => x.ModoAdministracionId,
                        principalTable: "ModoAdministracion",
                        principalColumn: "ModoAdministracionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    VentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrabajadorId = table.Column<int>(type: "int", nullable: false),
                    MedicamentoId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.VentaId);
                    table.ForeignKey(
                        name: "FK_Venta_Medicamento_MedicamentoId",
                        column: x => x.MedicamentoId,
                        principalTable: "Medicamento",
                        principalColumn: "MedicamentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Venta_Trabajador_TrabajadorId",
                        column: x => x.TrabajadorId,
                        principalTable: "Trabajador",
                        principalColumn: "TrabajadorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_LaboratorioId",
                table: "Medicamento",
                column: "LaboratorioId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_ModoAdministracionId",
                table: "Medicamento",
                column: "ModoAdministracionId");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_MedicamentoId",
                table: "Venta",
                column: "MedicamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Venta_TrabajadorId",
                table: "Venta",
                column: "TrabajadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Venta");

            migrationBuilder.DropTable(
                name: "Medicamento");

            migrationBuilder.DropTable(
                name: "Trabajador");

            migrationBuilder.DropTable(
                name: "Laboratorio");

            migrationBuilder.DropTable(
                name: "ModoAdministracion");
        }
    }
}
