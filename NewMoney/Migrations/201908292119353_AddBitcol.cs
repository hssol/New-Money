namespace NewMoney.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBitcol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Bits", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Bits");
        }
    }
}
