using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobInsight.Migrations
{
    /// <inheritdoc />
    public partial class AddCvAnalysisEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCvUploads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    FileUrl = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    FileSizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    RawText = table.Column<string>(type: "text", nullable: false),
                    ParsedJobTitle = table.Column<string>(type: "text", nullable: false),
                    ParsedYearsOfExperience = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCvUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCvUploads_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppCvAnalyses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CvUploadId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetJobTitle = table.Column<string>(type: "text", nullable: false),
                    Strengths = table.Column<string>(type: "text", nullable: false),
                    Weaknesses = table.Column<string>(type: "text", nullable: false),
                    MissingSkills = table.Column<string>(type: "text", nullable: false),
                    MarketMatchScore = table.Column<decimal>(type: "numeric", nullable: false),
                    ReferencedJobCount = table.Column<int>(type: "integer", nullable: false),
                    ReferenceJobsFrom = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ReferenceJobsTo = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RawAiResponse = table.Column<string>(type: "text", nullable: false),
                    AiModel = table.Column<string>(type: "text", nullable: false),
                    AnalyzedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCvAnalyses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCvAnalyses_AppCvUploads_CvUploadId",
                        column: x => x.CvUploadId,
                        principalTable: "AppCvUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCvAnalyses_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppUserSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProficiencyLevel = table.Column<string>(type: "text", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    CvUploadId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserSkills", x => x.Id);
                    table.UniqueConstraint("AK_AppUserSkills_UserId_SkillId", x => new { x.UserId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_AppUserSkills_AppCvUploads_CvUploadId",
                        column: x => x.CvUploadId,
                        principalTable: "AppCvUploads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppUserSkills_AppSkills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "AppSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserSkills_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCvAnalyses_AnalyzedAt",
                table: "AppCvAnalyses",
                column: "AnalyzedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AppCvAnalyses_CvUploadId",
                table: "AppCvAnalyses",
                column: "CvUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCvAnalyses_UserId",
                table: "AppCvAnalyses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCvUploads_Status",
                table: "AppCvUploads",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_AppCvUploads_UploadedAt",
                table: "AppCvUploads",
                column: "UploadedAt");

            migrationBuilder.CreateIndex(
                name: "IX_AppCvUploads_UserId",
                table: "AppCvUploads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserSkills_CvUploadId",
                table: "AppUserSkills",
                column: "CvUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserSkills_SkillId",
                table: "AppUserSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserSkills_UserId",
                table: "AppUserSkills",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCvAnalyses");

            migrationBuilder.DropTable(
                name: "AppUserSkills");

            migrationBuilder.DropTable(
                name: "AppCvUploads");
        }
    }
}
