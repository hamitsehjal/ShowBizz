namespace HS2231A5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActorMediaItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActorMediaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.Binary(),
                        ContentType = c.String(maxLength: 200),
                        Caption = c.String(nullable: false, maxLength: 100),
                        Timestamp = c.DateTime(nullable: false),
                        StringId = c.String(nullable: false, maxLength: 100),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.ActorId)
                .Index(t => t.ActorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActorMediaItems", "ActorId", "dbo.Actors");
            DropIndex("dbo.ActorMediaItems", new[] { "ActorId" });
            DropTable("dbo.ActorMediaItems");
        }
    }
}
