namespace BeHired.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessStreams",
                c => new
                    {
                        BusinessStreamId = c.Int(nullable: false, identity: true),
                        business_stream_name = c.String(),
                    })
                .PrimaryKey(t => t.BusinessStreamId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                        BusinessStreamId = c.Int(nullable: false),
                        company_name = c.String(),
                        establishment_date = c.DateTime(nullable: false),
                        address = c.String(),
                        contact = c.String(),
                        company_website_url = c.String(),
                        description = c.String(),
                        company_image = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId)
                .ForeignKey("dbo.BusinessStreams", t => t.BusinessStreamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.BusinessStreamId);
            
            CreateTable(
                "dbo.JobPosts",
                c => new
                    {
                        CompanyId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false, identity: true),
                        job_title = c.String(),
                        period = c.String(),
                        last_application_date = c.DateTime(nullable: false),
                        experience = c.String(),
                        job_description = c.String(),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        ProfileId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        JobApplicationId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.JobApplicationId)
                .ForeignKey("dbo.JobPosts", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                        firstname = c.String(nullable: false),
                        lastname = c.String(nullable: false),
                        current_salary = c.String(),
                        currency = c.String(),
                    })
                .PrimaryKey(t => t.ProfileId)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.EducationDetails",
                c => new
                    {
                        EducationDetailId = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        certificate_degree_name = c.String(),
                        major = c.String(),
                        institute_university_name = c.String(),
                        starting_date = c.DateTime(nullable: false),
                        completion_date = c.DateTime(nullable: false),
                        percentage = c.Int(nullable: false),
                        cgpa = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.EducationDetailId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.ExperienceDetails",
                c => new
                    {
                        ExperienceDetailId = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        start_date = c.DateTime(nullable: false),
                        end_data = c.DateTime(nullable: false),
                        job_title = c.String(),
                        company_name = c.String(),
                        job_location_city = c.String(),
                        job_location_state = c.String(),
                        job_location_country = c.String(),
                        description = c.String(),
                    })
                .PrimaryKey(t => t.ExperienceDetailId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        skill_name = c.String(),
                        skill_level = c.String(),
                    })
                .PrimaryKey(t => t.SkillId)
                .ForeignKey("dbo.Profiles", t => t.ProfileId, cascadeDelete: true)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        user_type = c.String(),
                        date_of_birth = c.DateTime(nullable: false),
                        gender = c.String(),
                        contact = c.String(),
                        address = c.String(),
                        user_image = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        category_name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.JobSkillSets",
                c => new
                    {
                        JobSkillId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        skill = c.String(),
                        skill_level = c.String(),
                    })
                .PrimaryKey(t => t.JobSkillId)
                .ForeignKey("dbo.JobPosts", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Companies", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobSkillSets", "JobId", "dbo.JobPosts");
            DropForeignKey("dbo.JobPosts", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.JobPosts", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.JobApplications", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Profiles", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Skills", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.ExperienceDetails", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.EducationDetails", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.JobApplications", "JobId", "dbo.JobPosts");
            DropForeignKey("dbo.Companies", "BusinessStreamId", "dbo.BusinessStreams");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.JobSkillSets", new[] { "JobId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Skills", new[] { "ProfileId" });
            DropIndex("dbo.ExperienceDetails", new[] { "ProfileId" });
            DropIndex("dbo.EducationDetails", new[] { "ProfileId" });
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropIndex("dbo.JobApplications", new[] { "JobId" });
            DropIndex("dbo.JobApplications", new[] { "ProfileId" });
            DropIndex("dbo.JobPosts", new[] { "CategoryId" });
            DropIndex("dbo.JobPosts", new[] { "CompanyId" });
            DropIndex("dbo.Companies", new[] { "BusinessStreamId" });
            DropIndex("dbo.Companies", new[] { "Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.JobSkillSets");
            DropTable("dbo.Categories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Skills");
            DropTable("dbo.ExperienceDetails");
            DropTable("dbo.EducationDetails");
            DropTable("dbo.Profiles");
            DropTable("dbo.JobApplications");
            DropTable("dbo.JobPosts");
            DropTable("dbo.Companies");
            DropTable("dbo.BusinessStreams");
        }
    }
}
