namespace OnlineShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkoutfixed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckOutInformations", "FirstName", c => c.String());
            AddColumn("dbo.CheckOutInformations", "LastName", c => c.String());
            AddColumn("dbo.CheckOutInformations", "City", c => c.String());
            AddColumn("dbo.CheckOutInformations", "Adress", c => c.String());
            AddColumn("dbo.CheckOutInformations", "MobilePhone", c => c.String());
            AddColumn("dbo.CheckOutInformations", "EmailAdress", c => c.String());
            AlterColumn("dbo.CheckOutInformations", "ZipCode", c => c.String());
            DropColumn("dbo.CheckOutInformations", "Name");
            DropColumn("dbo.CheckOutInformations", "Family");
            DropColumn("dbo.CheckOutInformations", "Street");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CheckOutInformations", "Street", c => c.String());
            AddColumn("dbo.CheckOutInformations", "Family", c => c.String());
            AddColumn("dbo.CheckOutInformations", "Name", c => c.String());
            AlterColumn("dbo.CheckOutInformations", "ZipCode", c => c.Int(nullable: false));
            DropColumn("dbo.CheckOutInformations", "EmailAdress");
            DropColumn("dbo.CheckOutInformations", "MobilePhone");
            DropColumn("dbo.CheckOutInformations", "Adress");
            DropColumn("dbo.CheckOutInformations", "City");
            DropColumn("dbo.CheckOutInformations", "LastName");
            DropColumn("dbo.CheckOutInformations", "FirstName");
        }
    }
}
