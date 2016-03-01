#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(false, true),
                    FirstName = c.String(false, 50),
                    LastName = c.String(false, 50),
                    MiddleName = c.String(maxLength: 50),
                    BirthDate = c.DateTime(false),
                    RegistrationDate = c.DateTime(false),
                    NickName = c.String(),
                    Email = c.String(false),
                    Phone = c.String(),
                    Address = c.String(),
                    AccountStatus = c.String(),
                    AccountConfirmed = c.Boolean(false),
                    Gender = c.String(maxLength: 1),
                    Information = c.String(),
                    Photo = c.Binary(),
                    MimeType = c.String()
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Posts",
                c => new
                {
                    Id = c.Int(false, true),
                    Text = c.String(),
                    Status = c.String(maxLength: 30),
                    Position = c.String(),
                    TimePosted = c.DateTime(false),
                    Tag = c.String(false),
                    UserPosted_Id = c.Int()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserPosted_Id)
                .Index(t => t.UserPosted_Id);

            CreateTable(
                "dbo.PostAdditions",
                c => new
                {
                    Id = c.Int(false, true),
                    Data = c.Binary(),
                    MimeType = c.String(),
                    Information = c.String(),
                    Post_Id = c.Int()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);

            CreateTable(
                "dbo.UserUsers",
                c => new
                {
                    User_Id = c.Int(false),
                    User_Id1 = c.Int(false)
                })
                .PrimaryKey(t => new {t.User_Id, t.User_Id1})
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserPosted_Id", "dbo.Users");
            DropForeignKey("dbo.PostAdditions", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.UserUsers", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_Id", "dbo.Users");
            DropIndex("dbo.UserUsers", new[] {"User_Id1"});
            DropIndex("dbo.UserUsers", new[] {"User_Id"});
            DropIndex("dbo.PostAdditions", new[] {"Post_Id"});
            DropIndex("dbo.Posts", new[] {"UserPosted_Id"});
            DropTable("dbo.UserUsers");
            DropTable("dbo.PostAdditions");
            DropTable("dbo.Posts");
            DropTable("dbo.Users");
        }
    }
}