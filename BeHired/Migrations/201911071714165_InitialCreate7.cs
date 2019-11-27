namespace BeHired.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "linkedinProfileLink", c => c.String());
            AddColumn("dbo.Profiles", "facebooklink", c => c.String());
            AddColumn("dbo.Profiles", "twitterlink", c => c.String());
            AddColumn("dbo.Profiles", "instagramlink", c => c.String());
            AddColumn("dbo.Profiles", "githublink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "githublink");
            DropColumn("dbo.Profiles", "instagramlink");
            DropColumn("dbo.Profiles", "twitterlink");
            DropColumn("dbo.Profiles", "facebooklink");
            DropColumn("dbo.Profiles", "linkedinProfileLink");
        }
    }
}
