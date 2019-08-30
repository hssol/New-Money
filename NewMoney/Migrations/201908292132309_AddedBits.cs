namespace NewMoney.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBits : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Bits", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Bits", c => c.String());
        }
    }
}
