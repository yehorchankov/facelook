#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class Fixedshit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conversations", "User1_Id", "dbo.Users");
            DropForeignKey("dbo.Conversations", "User2_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.Users");
            DropForeignKey("dbo.Posts", "UserPosted_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "CommentOwner_Id", "dbo.Users");
            DropIndex("dbo.Conversations", new[] {"User1_Id"});
            DropIndex("dbo.Conversations", new[] {"User2_Id"});
            DropIndex("dbo.Messages", new[] {"Sender_Id"});
            DropIndex("dbo.Posts", new[] {"UserPosted_Id"});
            DropIndex("dbo.Comments", new[] {"CommentOwner_Id"});
            RenameColumn("dbo.Messages", "Sender_Id", "SenderId");
            RenameColumn("dbo.Posts", "UserPosted_Id", "UserPostedId");
            RenameColumn("dbo.Comments", "CommentOwner_Id", "CommentOwnerId");
            AddColumn("dbo.Conversations", "User1Nick", c => c.String());
            AddColumn("dbo.Conversations", "User2Nick", c => c.String());
            AlterColumn("dbo.Messages", "SenderId", c => c.Int(false));
            AlterColumn("dbo.Posts", "UserPostedId", c => c.Int(false));
            AlterColumn("dbo.Comments", "CommentOwnerId", c => c.Int(false));
            CreateIndex("dbo.Messages", "SenderId");
            CreateIndex("dbo.Posts", "UserPostedId");
            CreateIndex("dbo.Comments", "CommentOwnerId");
            AddForeignKey("dbo.Messages", "SenderId", "dbo.Users", "Id", true);
            AddForeignKey("dbo.Posts", "UserPostedId", "dbo.Users", "Id", true);
            AddForeignKey("dbo.Comments", "CommentOwnerId", "dbo.Users", "Id", true);
            DropColumn("dbo.Conversations", "User1_Id");
            DropColumn("dbo.Conversations", "User2_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.Conversations", "User2_Id", c => c.Int());
            AddColumn("dbo.Conversations", "User1_Id", c => c.Int());
            DropForeignKey("dbo.Comments", "CommentOwnerId", "dbo.Users");
            DropForeignKey("dbo.Posts", "UserPostedId", "dbo.Users");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropIndex("dbo.Comments", new[] {"CommentOwnerId"});
            DropIndex("dbo.Posts", new[] {"UserPostedId"});
            DropIndex("dbo.Messages", new[] {"SenderId"});
            AlterColumn("dbo.Comments", "CommentOwnerId", c => c.Int());
            AlterColumn("dbo.Posts", "UserPostedId", c => c.Int());
            AlterColumn("dbo.Messages", "SenderId", c => c.Int());
            DropColumn("dbo.Conversations", "User2Nick");
            DropColumn("dbo.Conversations", "User1Nick");
            RenameColumn("dbo.Comments", "CommentOwnerId", "CommentOwner_Id");
            RenameColumn("dbo.Posts", "UserPostedId", "UserPosted_Id");
            RenameColumn("dbo.Messages", "SenderId", "Sender_Id");
            CreateIndex("dbo.Comments", "CommentOwner_Id");
            CreateIndex("dbo.Posts", "UserPosted_Id");
            CreateIndex("dbo.Messages", "Sender_Id");
            CreateIndex("dbo.Conversations", "User2_Id");
            CreateIndex("dbo.Conversations", "User1_Id");
            AddForeignKey("dbo.Comments", "CommentOwner_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Posts", "UserPosted_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Messages", "Sender_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Conversations", "User2_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Conversations", "User1_Id", "dbo.Users", "Id");
        }
    }
}