namespace BeHired.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profiles", "currentpost", c => c.String());
            AddColumn("dbo.Profiles", "currentcompany", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profiles", "currentcompany");
            DropColumn("dbo.Profiles", "currentpost");
        }
    }
}
