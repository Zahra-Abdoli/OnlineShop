namespace OnlineShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckOutInformations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Family = c.String(),
                        Street = c.String(),
                        ZipCode = c.Int(nullable: false),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CheckOutInformations");
        }
    }
}
