namespace BeHired.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class w : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JobApplications", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JobApplications", "status");
        }
    }
}
