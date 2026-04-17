using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobInsight.Migrations
{
    /// <inheritdoc />
    public partial class retry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppBenefits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    IconName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TotalCompaniesOffering = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBenefits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameSlug = table.Column<string>(type: "text", nullable: false),
                    ExternalId = table.Column<string>(type: "text", nullable: false),
                    SourceWebsite = table.Column<string>(type: "text", nullable: false),
                    Industry = table.Column<string>(type: "text", nullable: false),
                    CompanySize = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: false),
                    LogoUrl = table.Column<string>(type: "text", nullable: false),
                    HeadquarterLocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: false),
                    LinkedInUrl = table.Column<string>(type: "text", nullable: false),
                    FacebookUrl = table.Column<string>(type: "text", nullable: false),
                    TotalJobsPosted = table.Column<int>(type: "integer", nullable: false),
                    ActiveJobsCount = table.Column<int>(type: "integer", nullable: false),
                    AverageSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    WhyJoinUs = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: true),
                    ReviewCount = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_AppCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCrawlHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceWebsite = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    JobsFound = table.Column<int>(type: "integer", nullable: false),
                    JobsCreated = table.Column<int>(type: "integer", nullable: false),
                    JobsUpdated = table.Column<int>(type: "integer", nullable: false),
                    JobsSkipped = table.Column<int>(type: "integer", nullable: false),
                    ErrorCount = table.Column<int>(type: "integer", nullable: false),
                    ErrorDetails = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    PagesProcessed = table.Column<int>(type: "integer", nullable: false),
                    DurationSeconds = table.Column<int>(type: "integer", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCrawlHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    District = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric", nullable: true),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: true),
                    TotalJobs = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMarketSnapshots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SnapshotDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalActiveJobs = table.Column<int>(type: "integer", nullable: false),
                    NewJobsToday = table.Column<int>(type: "integer", nullable: false),
                    ExpiredJobsToday = table.Column<int>(type: "integer", nullable: false),
                    AverageSalaryUSD = table.Column<decimal>(type: "numeric", nullable: true),
                    MedianSalaryUSD = table.Column<decimal>(type: "numeric", nullable: true),
                    TopSkills = table.Column<string>(type: "text", nullable: false),
                    TopCompanies = table.Column<string>(type: "text", nullable: false),
                    TopLocations = table.Column<string>(type: "text", nullable: false),
                    RemoteJobsPercentage = table.Column<decimal>(type: "numeric", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMarketSnapshots", x => x.Id);
                    table.UniqueConstraint("AK_AppMarketSnapshots_SnapshotDate", x => x.SnapshotDate);
                });

            migrationBuilder.CreateTable(
                name: "AppSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    SubCategory = table.Column<string>(type: "text", nullable: false),
                    Aliases = table.Column<string>(type: "text", nullable: false),
                    RelatedSkills = table.Column<string>(type: "text", nullable: false),
                    TotalJobMentions = table.Column<int>(type: "integer", nullable: false),
                    TrendingScore = table.Column<decimal>(type: "numeric", nullable: false),
                    IconUrl = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSkills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserJobAlerts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Keywords = table.Column<string>(type: "text", nullable: false),
                    SkillIds = table.Column<string>(type: "text", nullable: false),
                    LocationIds = table.Column<string>(type: "text", nullable: false),
                    MinSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    ExperienceLevel = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Frequency = table.Column<string>(type: "text", nullable: false),
                    LastSentAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
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
                    table.PrimaryKey("PK_AppUserJobAlerts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalId = table.Column<string>(type: "text", nullable: false),
                    SourceWebsite = table.Column<string>(type: "text", nullable: false),
                    SourceUrl = table.Column<string>(type: "text", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkMode = table.Column<string>(type: "text", nullable: false),
                    SalaryMin = table.Column<decimal>(type: "numeric", nullable: true),
                    SalaryMax = table.Column<decimal>(type: "numeric", nullable: true),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    SalaryDisplay = table.Column<string>(type: "text", nullable: false),
                    IsNegotiable = table.Column<bool>(type: "boolean", nullable: false),
                    ExperienceLevel = table.Column<string>(type: "text", nullable: false),
                    YearsOfExperienceMin = table.Column<int>(type: "integer", nullable: true),
                    YearsOfExperienceMax = table.Column<int>(type: "integer", nullable: true),
                    JobType = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Requirements = table.Column<string>(type: "text", nullable: false),
                    Benefits = table.Column<string>(type: "text", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastCrawledAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsExpired = table.Column<bool>(type: "boolean", nullable: false),
                    ViewCount = table.Column<int>(type: "integer", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_AppJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppJobs_AppCompanies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "AppCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppJobs_AppLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "AppLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AppSkillTrends",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    JobMentions = table.Column<int>(type: "integer", nullable: false),
                    AverageSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    MedianSalary = table.Column<decimal>(type: "numeric", nullable: true),
                    GrowthRate = table.Column<decimal>(type: "numeric", nullable: true),
                    JuniorJobsCount = table.Column<int>(type: "integer", nullable: false),
                    MidJobsCount = table.Column<int>(type: "integer", nullable: false),
                    SeniorJobsCount = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSkillTrends", x => x.Id);
                    table.UniqueConstraint("AK_AppSkillTrends_SkillId_Year_Month", x => new { x.SkillId, x.Year, x.Month });
                    table.ForeignKey(
                        name: "FK_AppSkillTrends_AppSkills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "AppSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppJobBenefits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    BenefitId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomValue = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppJobBenefits", x => x.Id);
                    table.UniqueConstraint("AK_AppJobBenefits_JobId_BenefitId", x => new { x.JobId, x.BenefitId });
                    table.ForeignKey(
                        name: "FK_AppJobBenefits_AppBenefits_BenefitId",
                        column: x => x.BenefitId,
                        principalTable: "AppBenefits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppJobBenefits_AppJobs_JobId",
                        column: x => x.JobId,
                        principalTable: "AppJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppJobSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    IsPrimarySkill = table.Column<bool>(type: "boolean", nullable: false),
                    ProficiencyLevel = table.Column<string>(type: "text", nullable: false),
                    MentionCount = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppJobSkills", x => x.Id);
                    table.UniqueConstraint("AK_AppJobSkills_JobId_SkillId", x => new { x.JobId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_AppJobSkills_AppJobs_JobId",
                        column: x => x.JobId,
                        principalTable: "AppJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppJobSkills_AppSkills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "AppSkills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserSavedJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserSavedJobs", x => x.Id);
                    table.UniqueConstraint("AK_AppUserSavedJobs_UserId_JobId", x => new { x.UserId, x.JobId });
                    table.ForeignKey(
                        name: "FK_AppUserSavedJobs_AppJobs_JobId",
                        column: x => x.JobId,
                        principalTable: "AppJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppBenefits_NormalizedName",
                table: "AppBenefits",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCompanies_Industry",
                table: "AppCompanies",
                column: "Industry");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompanies_IsDeleted",
                table: "AppCompanies",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_AppCompanies_NameSlug",
                table: "AppCompanies",
                column: "NameSlug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCrawlHistories_SourceWebsite",
                table: "AppCrawlHistories",
                column: "SourceWebsite");

            migrationBuilder.CreateIndex(
                name: "IX_AppCrawlHistories_StartTime",
                table: "AppCrawlHistories",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_AppJobBenefits_BenefitId",
                table: "AppJobBenefits",
                column: "BenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_AppJobBenefits_JobId",
                table: "AppJobBenefits",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_AppJobs_CompanyId",
                table: "AppJobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AppJobs_ExperienceLevel",
                table: "AppJobs",
                column: "ExperienceLevel");

            migrationBuilder.CreateIndex(
                name: "IX_AppJobs_IsActive",
                table: "AppJobs",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AppJobs_LocationId",
                table: "AppJobs",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppJobs_PostedDate",
                table: "AppJobs",
                column: "PostedDate");

            migrationBuilder.CreateIndex(
                name: "IX_AppJobs_SourceWebsite",
                table: "AppJobs",
                column: "SourceWebsite");

            migrationBuilder.CreateIndex(
                name: "IX_AppJobSkills_JobId",
                table: "AppJobSkills",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_AppJobSkills_SkillId",
                table: "AppJobSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_AppLocations_City",
                table: "AppLocations",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_AppLocations_Slug",
                table: "AppLocations",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppMarketSnapshots_SnapshotDate",
                table: "AppMarketSnapshots",
                column: "SnapshotDate");

            migrationBuilder.CreateIndex(
                name: "IX_AppSkills_Category",
                table: "AppSkills",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_AppSkills_NormalizedName",
                table: "AppSkills",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSkillTrends_SkillId_Year_Month",
                table: "AppSkillTrends",
                columns: new[] { "SkillId", "Year", "Month" });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserJobAlerts_IsActive",
                table: "AppUserJobAlerts",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserJobAlerts_UserId",
                table: "AppUserJobAlerts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserSavedJobs_JobId",
                table: "AppUserSavedJobs",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserSavedJobs_UserId",
                table: "AppUserSavedJobs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCrawlHistories");

            migrationBuilder.DropTable(
                name: "AppJobBenefits");

            migrationBuilder.DropTable(
                name: "AppJobSkills");

            migrationBuilder.DropTable(
                name: "AppMarketSnapshots");

            migrationBuilder.DropTable(
                name: "AppSkillTrends");

            migrationBuilder.DropTable(
                name: "AppUserJobAlerts");

            migrationBuilder.DropTable(
                name: "AppUserSavedJobs");

            migrationBuilder.DropTable(
                name: "AppBenefits");

            migrationBuilder.DropTable(
                name: "AppSkills");

            migrationBuilder.DropTable(
                name: "AppJobs");

            migrationBuilder.DropTable(
                name: "AppCompanies");

            migrationBuilder.DropTable(
                name: "AppLocations");
        }
    }
}
