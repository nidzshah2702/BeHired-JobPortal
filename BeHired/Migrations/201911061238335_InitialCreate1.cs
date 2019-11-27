namespace BeHired.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "date_of_birth", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "date_of_birth", c => c.DateTime(nullable: false));
        }
    }
}
