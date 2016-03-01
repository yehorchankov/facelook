#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class Many_To_Many_Problem_Resolver_Added : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_Id1", "dbo.Users");
            DropIndex("dbo.UserUsers", new[] {"User_Id"});
            DropIndex("dbo.UserUsers", new[] {"User_Id1"});
            CreateTable(
                "dbo.ManyToManyResolvers",
                c => new
                {
                    Id = c.Int(false, true)
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Users", "User_Id", c => c.Int());
            AddColumn("dbo.Users", "FriendsToConfirm_Id", c => c.Int());
            CreateIndex("dbo.Users", "User_Id");
            CreateIndex("dbo.Users", "FriendsToConfirm_Id");
            AddForeignKey("dbo.Users", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "FriendsToConfirm_Id", "dbo.ManyToManyResolvers", "Id");
            DropTable("dbo.UserUsers");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.UserUsers",
                c => new
                {
                    User_Id = c.Int(false),
                    User_Id1 = c.Int(false)
                })
                .PrimaryKey(t => new {t.User_Id, t.User_Id1});

            DropForeignKey("dbo.Users", "FriendsToConfirm_Id", "dbo.ManyToManyResolvers");
            DropForeignKey("dbo.Users", "User_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] {"FriendsToConfirm_Id"});
            DropIndex("dbo.Users", new[] {"User_Id"});
            DropColumn("dbo.Users", "FriendsToConfirm_Id");
            DropColumn("dbo.Users", "User_Id");
            DropTable("dbo.ManyToManyResolvers");
            CreateIndex("dbo.UserUsers", "User_Id1");
            CreateIndex("dbo.UserUsers", "User_Id");
            AddForeignKey("dbo.UserUsers", "User_Id1", "dbo.Users", "Id");
            AddForeignKey("dbo.UserUsers", "User_Id", "dbo.Users", "Id");
        }
    }
}