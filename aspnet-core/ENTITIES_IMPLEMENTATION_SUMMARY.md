# JobInsight Domain Entities - Implementation Summary

## Overview
Created 11 domain entities following ABP (ASP.NET Boilerplate) conventions and integrated them into the EntityFrameworkCore DbContext.

## Created Entities

### Core Domain Entities

#### 1. **Company** ([src/JobInsight.Domain/Jobs/Company.cs](src/JobInsight.Domain/Jobs/Company.cs))
- Base class: `FullAuditedAggregateRoot<Guid>`
- Represents companies posting jobs
- Key properties: Name, NameSlug, Industry, CompanySize, Website, LogoUrl, Rating, etc.
- Full audit trail (Creation, Modification, Deletion tracking)

#### 2. **Location** ([src/JobInsight.Domain/Jobs/Location.cs](src/JobInsight.Domain/Jobs/Location.cs))
- Base class: `CreationAuditedAggregateRoot<Guid>`
- Represents geographic locations for job postings
- Key properties: City, District, Country, DisplayName, Slug, Latitude, Longitude
- Soft delete support

#### 3. **Skill** ([src/JobInsight.Domain/Jobs/Skill.cs](src/JobInsight.Domain/Jobs/Skill.cs))
- Base class: `CreationAuditedAggregateRoot<Guid>`
- Represents technical and soft skills
- Key properties: Name, NormalizedName, Category, SubCategory, Aliases, TrendingScore
- Unique normalized name for search optimization

#### 4. **Benefit** ([src/JobInsight.Domain/Jobs/Benefit.cs](src/JobInsight.Domain/Jobs/Benefit.cs))
- Base class: `CreationAuditedAggregateRoot<Guid>`
- Represents job benefits/perks
- Key properties: Name, NormalizedName, Category, IconName, Description
- Unique normalized name for consistency

#### 5. **Job** ([src/JobInsight.Domain/Jobs/Job.cs](src/JobInsight.Domain/Jobs/Job.cs))
- Base class: `FullAuditedAggregateRoot<Guid>`
- Represents job postings
- Key properties: Title, CompanyId, LocationId, SalaryMin, SalaryMax, ExperienceLevel, WorkMode, etc.
- Full audit trail support
- Navigation properties: Company, Location, JobSkills, JobBenefits, SavedByUsers
- Soft delete support

### Junction/Relationship Entities

#### 6. **JobSkill** ([src/JobInsight.Domain/Jobs/JobSkill.cs](src/JobInsight.Domain/Jobs/JobSkill.cs))
- Base class: `CreationAuditedEntity<Guid>`
- Many-to-many relationship between Job and Skill
- Key properties: JobId, SkillId, IsRequired, IsPrimarySkill, ProficiencyLevel
- Enforces unique constraint on (JobId, SkillId)

#### 7. **JobBenefit** ([src/JobInsight.Domain/Jobs/JobBenefit.cs](src/JobInsight.Domain/Jobs/JobBenefit.cs))
- Base class: `CreationAuditedEntity<Guid>`
- Many-to-many relationship between Job and Benefit
- Key properties: JobId, BenefitId, CustomValue
- Enforces unique constraint on (JobId, BenefitId)

### Analytics/Tracking Entities

#### 8. **CrawlHistory** ([src/JobInsight.Domain/Jobs/CrawlHistory.cs](src/JobInsight.Domain/Jobs/CrawlHistory.cs))
- Base class: `CreationAuditedAggregateRoot<Guid>`
- Tracks job scraping/crawling sessions
- Key properties: SourceWebsite, StartTime, EndTime, JobsFound, JobsCreated, JobsUpdated, Status, DurationSeconds
- Performance metrics tracking

#### 9. **SkillTrend** ([src/JobInsight.Domain/Jobs/SkillTrend.cs](src/JobInsight.Domain/Jobs/SkillTrend.cs))
- Base class: `CreationAuditedAggregateRoot<Guid>`
- Monthly snapshot of skill trends
- Key properties: SkillId, Year, Month, JobMentions, AverageSalary, GrowthRate, JuniorJobsCount, etc.
- Unique constraint on (SkillId, Year, Month)

#### 10. **MarketSnapshot** ([src/JobInsight.Domain/Jobs/MarketSnapshot.cs](src/JobInsight.Domain/Jobs/MarketSnapshot.cs))
- Base class: `CreationAuditedAggregateRoot<Guid>`
- Daily market metrics snapshot
- Key properties: SnapshotDate, TotalActiveJobs, NewJobsToday, AverageSalaryUSD, TopSkills, TopCompanies, TopLocations
- Unique constraint on SnapshotDate

### User Feature Entities

#### 11. **UserSavedJob** ([src/JobInsight.Domain/Jobs/UserSavedJob.cs](src/JobInsight.Domain/Jobs/UserSavedJob.cs))
- Base class: `CreationAuditedEntity<Guid>`
- Tracks user-saved job listings
- Key properties: UserId, JobId, Notes
- Unique constraint on (UserId, JobId)

#### 12. **UserJobAlert** ([src/JobInsight.Domain/Jobs/UserJobAlert.cs](src/JobInsight.Domain/Jobs/UserJobAlert.cs))
- Base class: `FullAuditedAggregateRoot<Guid>`
- Represents job alert subscriptions for users
- Key properties: UserId, Keywords, SkillIds, LocationIds, MinSalary, ExperienceLevel, IsActive, Frequency
- Full audit trail for tracking changes

## DbContext Configuration

Updated [src/JobInsight.EntityFrameworkCore/EntityFrameworkCore/JobInsightDbContext.cs](src/JobInsight.EntityFrameworkCore/EntityFrameworkCore/JobInsightDbContext.cs):

### DbSet Properties Added:
```csharp
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
```

### Entity Configuration Features:
- ✅ Table naming with ABP conventions (DbTablePrefix + DbSchema)
- ✅ Automatic configuration via `ConfigureByConvention()`
- ✅ Unique indexes on slug/normalized fields
- ✅ Composite indexes for common query patterns
- ✅ Foreign key relationships with proper cascade delete rules
- ✅ Alternate keys (unique constraints) on business keys
- ✅ Soft delete support where applicable

## Key Design Decisions

1. **Aggregate Roots**: Used `FullAuditedAggregateRoot` for entities that are modified (Company, Job, UserJobAlert) and `CreationAuditedAggregateRoot` for read-heavy or immutable entities.

2. **Soft Deletes**: Company, Location, and Job support soft deletes via `IsDeleted` property.

3. **Navigation Properties**: Configured one-to-many and many-to-many relationships with proper foreign key handling.

4. **Indexes**: Created indexes matching the PostgreSQL schema design for optimal query performance:
   - Unique indexes on slug/normalized fields
   - Composite indexes for common filter combinations
   - Foreign key indexes automatically

5. **Cascade Delete**: Configured appropriate cascade behaviors:
   - Job → JobSkill/JobBenefit: Cascade
   - Job → Location: Set Null (locations can exist without jobs)
   - Trend/Snapshot records: Cascade to maintain referential integrity

## Next Steps

1. Create Entity Framework Core migrations: `dotnet ef migrations add AddJobInsightEntities`
2. Update the database: `dotnet ef database update`
3. Create repositories for aggregate roots
4. Create Application Layer DTOs and mappings
5. Implement Domain Services as needed

## File Structure
```
src/JobInsight.Domain/Jobs/
├── Company.cs
├── Location.cs
├── Job.cs
├── Skill.cs
├── Benefit.cs
├── JobSkill.cs
├── JobBenefit.cs
├── CrawlHistory.cs
├── SkillTrend.cs
├── MarketSnapshot.cs
├── UserSavedJob.cs
└── UserJobAlert.cs

src/JobInsight.EntityFrameworkCore/EntityFrameworkCore/
└── JobInsightDbContext.cs (Updated)
```
