namespace BeHired.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class y : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplications", "neworold", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobApplications", "neworold");
        }
    }
}
