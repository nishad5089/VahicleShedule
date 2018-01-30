namespace Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SheduleVahicles", "SheduleDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SheduleVahicles", "SheduleDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
