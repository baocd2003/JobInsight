using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using JobInsight.Entities;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace JobInsight.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class JobInsightDbContext :
    AbpDbContext<JobInsightDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    // Job Management Entities
    public DbSet<Company> Companies { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<JobSkill> JobSkills { get; set; }
    public DbSet<Benefit> Benefits { get; set; }
    public DbSet<JobBenefit> JobBenefits { get; set; }

    // Analytics Entities
    public DbSet<CrawlHistory> CrawlHistories { get; set; }
    public DbSet<SkillTrend> SkillTrends { get; set; }
    public DbSet<MarketSnapshot> MarketSnapshots { get; set; }

    // User Features
    public DbSet<UserSavedJob> UserSavedJobs { get; set; }
    public DbSet<UserJobAlert> UserJobAlerts { get; set; }
    public DbSet<UserSkill> UserSkills { get; set; }

    // CV Analysis
    public DbSet<CvUpload> CvUploads { get; set; }
    public DbSet<CvAnalysis> CvAnalyses { get; set; }

    // Custom App Users
    public DbSet<AppUser> AppUsers { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public JobInsightDbContext(DbContextOptions<JobInsightDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        // Configure Companies
        builder.Entity<Company>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "Companies", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.NameSlug).IsUnique();
            b.HasIndex(x => x.Industry);
            b.HasIndex(x => x.IsDeleted).IsUnique(false);
        });

        // Configure Locations
        builder.Entity<Location>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "Locations", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.City);
            b.HasIndex(x => x.Slug).IsUnique();
        });

        // Configure Skills
        builder.Entity<Skill>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "Skills", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.NormalizedName).IsUnique();
            b.HasIndex(x => x.Category);
        });

        // Configure Benefits
        builder.Entity<Benefit>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "Benefits", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.NormalizedName).IsUnique();
        });

        // Configure Jobs
        builder.Entity<Job>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "Jobs", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.Location)
                .WithMany()
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.SetNull);
            b.HasMany(x => x.JobSkills)
                .WithOne(x => x.Job)
                .HasForeignKey(x => x.JobId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.JobBenefits)
                .WithOne(x => x.Job)
                .HasForeignKey(x => x.JobId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasMany(x => x.SavedByUsers)
                .WithOne(x => x.Job)
                .HasForeignKey(x => x.JobId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasIndex(x => x.CompanyId);
            b.HasIndex(x => x.PostedDate);
            b.HasIndex(x => x.IsActive);
            b.HasIndex(x => x.ExperienceLevel);
            b.HasIndex(x => x.SourceWebsite);
            b.HasIndex(x => x.LocationId);
        });

        // Configure JobSkills
        builder.Entity<JobSkill>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "JobSkills", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Job)
                .WithMany(x => x.JobSkills)
                .HasForeignKey(x => x.JobId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.Skill)
                .WithMany()
                .HasForeignKey(x => x.SkillId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasIndex(x => x.JobId);
            b.HasIndex(x => x.SkillId);
            b.HasAlternateKey(x => new { x.JobId, x.SkillId });
        });

        // Configure JobBenefits
        builder.Entity<JobBenefit>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "JobBenefits", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Job)
                .WithMany(x => x.JobBenefits)
                .HasForeignKey(x => x.JobId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.Benefit)
                .WithMany()
                .HasForeignKey(x => x.BenefitId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasIndex(x => x.JobId);
            b.HasIndex(x => x.BenefitId);
            b.HasAlternateKey(x => new { x.JobId, x.BenefitId });
        });

        // Configure CrawlHistories
        builder.Entity<CrawlHistory>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "CrawlHistories", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.SourceWebsite);
            b.HasIndex(x => x.StartTime);
        });

        // Configure SkillTrends
        builder.Entity<SkillTrend>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "SkillTrends", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Skill)
                .WithMany()
                .HasForeignKey(x => x.SkillId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasIndex(x => new { x.SkillId, x.Year, x.Month });
            b.HasAlternateKey(x => new { x.SkillId, x.Year, x.Month });
        });

        // Configure MarketSnapshots
        builder.Entity<MarketSnapshot>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "MarketSnapshots", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.SnapshotDate);
            b.HasAlternateKey(x => x.SnapshotDate);
        });

        // Configure UserSavedJobs
        builder.Entity<UserSavedJob>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "UserSavedJobs", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.Job)
                .WithMany(x => x.SavedByUsers)
                .HasForeignKey(x => x.JobId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasIndex(x => x.UserId);
            b.HasIndex(x => x.JobId);
            b.HasAlternateKey(x => new { x.UserId, x.JobId });
        });

        // Configure UserJobAlerts
        builder.Entity<UserJobAlert>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "UserJobAlerts", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.UserId);
            b.HasIndex(x => x.IsActive);
        });

        // Configure AppUsers (custom user table)
        builder.Entity<AppUser>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "Users", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasIndex(x => x.UserName).IsUnique();
            b.HasIndex(x => x.EmailAddress);
        });

        // Configure CvUploads
        builder.Entity<CvUpload>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "CvUploads", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            b.Property(x => x.RawText).HasColumnType("text");
            b.HasIndex(x => x.UserId);
            b.HasIndex(x => x.Status);
            b.HasIndex(x => x.UploadedAt);
        });

        // Configure UserSkills
        builder.Entity<UserSkill>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "UserSkills", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.Skill)
                .WithMany()
                .HasForeignKey(x => x.SkillId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.CvUpload)
                .WithMany()
                .HasForeignKey(x => x.CvUploadId)
                .OnDelete(DeleteBehavior.NoAction);
            b.HasIndex(x => x.UserId);
            b.HasIndex(x => x.SkillId);
            b.HasAlternateKey(x => new { x.UserId, x.SkillId });
        });

        // Configure CvAnalyses
        builder.Entity<CvAnalysis>(b =>
        {
            b.ToTable(JobInsightConsts.DbTablePrefix + "CvAnalyses", JobInsightConsts.DbSchema);
            b.ConfigureByConvention();
            b.HasOne(x => x.CvUpload)
                .WithMany()
                .HasForeignKey(x => x.CvUploadId)
                .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            b.Property(x => x.Strengths).HasColumnType("text");
            b.Property(x => x.Weaknesses).HasColumnType("text");
            b.Property(x => x.MissingSkills).HasColumnType("text");
            b.Property(x => x.RawAiResponse).HasColumnType("text");
            b.HasIndex(x => x.UserId);
            b.HasIndex(x => x.CvUploadId);
            b.HasIndex(x => x.AnalyzedAt);
        });
    }
}
