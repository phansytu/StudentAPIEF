using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentAPIw6.Context.Migrations
{
    /// <inheritdoc />
    public partial class createNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoMon",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maBM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tenBM = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoMon", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "LopHoc",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaLop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenLop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChuyenNganh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    maBM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoMonid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LopHoc_BoMon_BoMonid",
                        column: x => x.BoMonid,
                        principalTable: "BoMon",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaSV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiemTB = table.Column<decimal>(type: "float", nullable: false),
                    MaLop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lopHocId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SinhVien_LopHoc_lopHocId",
                        column: x => x.lopHocId,
                        principalTable: "LopHoc",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_BoMonid",
                table: "LopHoc",
                column: "BoMonid");

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_lopHocId",
                table: "SinhVien",
                column: "lopHocId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "LopHoc");

            migrationBuilder.DropTable(
                name: "BoMon");
        }
    }
}
