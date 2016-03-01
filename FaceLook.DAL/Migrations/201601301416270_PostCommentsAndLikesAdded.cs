#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class PostCommentsAndLikesAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Int(false, true),
                    Text = c.String(false),
                    TimeCommented = c.DateTime(false),
                    CommentOwner_Id = c.Int(),
                    Post_Id = c.Int()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CommentOwner_Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.CommentOwner_Id)
                .Index(t => t.Post_Id);

            AddColumn("dbo.Posts", "Likes", c => c.Int(false));
        }

        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Comments", "CommentOwner_Id", "dbo.Users");
            DropIndex("dbo.Comments", new[] {"Post_Id"});
            DropIndex("dbo.Comments", new[] {"CommentOwner_Id"});
            DropColumn("dbo.Posts", "Likes");
            DropTable("dbo.Comments");
        }
    }
}