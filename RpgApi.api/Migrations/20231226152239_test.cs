using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAcesso", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 12, 26, 12, 22, 39, 787, DateTimeKind.Local).AddTicks(3042), new byte[] { 144, 32, 111, 35, 51, 96, 169, 159, 124, 108, 95, 163, 201, 191, 235, 97, 60, 105, 212, 233, 25, 219, 198, 157, 176, 162, 177, 179, 98, 73, 232, 211 }, new byte[] { 51, 116, 243, 10, 11, 248, 242, 225, 132, 138, 87, 22, 34, 168, 254, 18, 91, 255, 150, 47, 252, 165, 92, 182, 56, 85, 238, 126, 73, 43, 38, 152 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAcesso", "PasswordHash", "PasswordSalt" },
                values: new object[] { new DateTime(2023, 12, 26, 12, 17, 33, 253, DateTimeKind.Local).AddTicks(1156), new byte[] { 178, 124, 106, 117, 161, 113, 246, 186, 208, 113, 61, 193, 123, 115, 166, 200, 200, 34, 60, 195, 251, 239, 147, 23, 213, 89, 231, 17, 120, 199, 29, 234 }, new byte[] { 144, 110, 50, 120, 212, 143, 201, 194, 16, 216, 19, 241, 127, 24, 176, 68, 234, 117, 61, 126, 31, 100, 237, 167, 55, 111, 65, 201, 214, 9, 164, 101 } });
        }
    }
}
