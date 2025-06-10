using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GoKeep.Repository.Migrations
{
    /// <inheritdoc />
    public partial class INITIAL_MAKE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersKeep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    firstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    lastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    passwordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UsersKee__3214EC07AFD68A99", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "NOW()"),
                    isActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Labels__3213E83F9116D622", x => x.id);
                    table.ForeignKey(
                        name: "FK__Labels__UserId__40058253",
                        column: x => x.UserId,
                        principalTable: "UsersKeep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    isPinned = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    isArchived = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    isTrashed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleteForever = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notes__3213E83F66741E52", x => x.id);
                    table.ForeignKey(
                        name: "FK__Notes__userId__3864608B",
                        column: x => x.userId,
                        principalTable: "UsersKeep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Token = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_UsersKeep_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersKeep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotesLabel",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    noteId = table.Column<int>(type: "integer", nullable: false),
                    labelId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NotesLab__3213E83F5244A2E5", x => x.id);
                    table.ForeignKey(
                        name: "FK__NotesLabe__label__6AEFE058",
                        column: x => x.labelId,
                        principalTable: "Labels",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__NotesLabe__noteI__69FBBC1F",
                        column: x => x.noteId,
                        principalTable: "Notes",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__NotesLabel__UsersKeep__on__UserId",
                        column: x => x.UserId,
                        principalTable: "UsersKeep",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "UniqueActiveLabelPerUser",
                table: "Labels",
                columns: new[] { "UserId", "name", "isActive" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_userId",
                table: "Notes",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesLabel_labelId",
                table: "NotesLabel",
                column: "labelId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesLabel_noteId",
                table: "NotesLabel",
                column: "noteId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesLabel_UserId",
                table: "NotesLabel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_Token",
                table: "UserRefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                table: "UserRefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__UsersKeep__email",
                table: "UsersKeep",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotesLabel");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "UsersKeep");
        }
    }
}
