namespace OAuthIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RefreshTokens");
        }
    }
}
