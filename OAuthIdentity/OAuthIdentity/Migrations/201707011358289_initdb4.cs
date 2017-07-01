namespace OAuthIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClientId = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clients", "ClientUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Users", "ClientUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Clients", "ClientUser_Id");
            CreateIndex("dbo.Users", "ClientUser_Id");
            AddForeignKey("dbo.Clients", "ClientUser_Id", "dbo.ClientUsers", "Id");
            AddForeignKey("dbo.Users", "ClientUser_Id", "dbo.ClientUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ClientUser_Id", "dbo.ClientUsers");
            DropForeignKey("dbo.Clients", "ClientUser_Id", "dbo.ClientUsers");
            DropIndex("dbo.Users", new[] { "ClientUser_Id" });
            DropIndex("dbo.Clients", new[] { "ClientUser_Id" });
            DropColumn("dbo.Users", "ClientUser_Id");
            DropColumn("dbo.Clients", "ClientUser_Id");
            DropTable("dbo.ClientUsers");
        }
    }
}
