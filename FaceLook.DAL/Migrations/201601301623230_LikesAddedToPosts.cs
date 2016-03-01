#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class LikesAddedToPosts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "UserPosted_Id", "dbo.Users");
            AddColumn("dbo.Users", "Post_Id", c => c.Int());
            AddColumn("dbo.Posts", "User_Id", c => c.Int());
            CreateIndex("dbo.Users", "Post_Id");
            CreateIndex("dbo.Posts", "User_Id");
            AddForeignKey("dbo.Users", "Post_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.Posts", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Posts", "Likes");
        }

        public override void Down()
        {
            AddColumn("dbo.Posts", "Likes", c => c.Int(false));
            DropForeignKey("dbo.Posts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Post_Id", "dbo.Posts");
            DropIndex("dbo.Posts", new[] {"User_Id"});
            DropIndex("dbo.Users", new[] {"Post_Id"});
            DropColumn("dbo.Posts", "User_Id");
            DropColumn("dbo.Users", "Post_Id");
            AddForeignKey("dbo.Posts", "UserPosted_Id", "dbo.Users", "Id");
        }
    }
}