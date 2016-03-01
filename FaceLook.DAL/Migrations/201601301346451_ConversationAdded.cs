#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class ConversationAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                {
                    Id = c.Int(false, true),
                    TimeCreated = c.DateTime(false),
                    User1_Id = c.Int(),
                    User2_Id = c.Int(),
                    User_Id = c.Int()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User1_Id)
                .ForeignKey("dbo.Users", t => t.User2_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User1_Id)
                .Index(t => t.User2_Id)
                .Index(t => t.User_Id);

            CreateTable(
                "dbo.Messages",
                c => new
                {
                    Id = c.Int(false, true),
                    Text = c.String(false),
                    Seen = c.Boolean(false),
                    TimeSent = c.DateTime(false),
                    Sender_Id = c.Int(),
                    Conversation_Id = c.Int()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Sender_Id)
                .ForeignKey("dbo.Conversations", t => t.Conversation_Id)
                .Index(t => t.Sender_Id)
                .Index(t => t.Conversation_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Conversations", "User2_Id", "dbo.Users");
            DropForeignKey("dbo.Conversations", "User1_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Conversation_Id", "dbo.Conversations");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.Users");
            DropIndex("dbo.Messages", new[] {"Conversation_Id"});
            DropIndex("dbo.Messages", new[] {"Sender_Id"});
            DropIndex("dbo.Conversations", new[] {"User_Id"});
            DropIndex("dbo.Conversations", new[] {"User2_Id"});
            DropIndex("dbo.Conversations", new[] {"User1_Id"});
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
        }
    }
}