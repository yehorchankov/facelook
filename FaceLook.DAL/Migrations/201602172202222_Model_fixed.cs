#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class Model_fixed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FriendshipRequests",
                c => new
                {
                    Id = c.Int(false, true),
                    Login = c.String(),
                    User_Id = c.Int()
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.FriendshipRequests", "User_Id", "dbo.Users");
            DropIndex("dbo.FriendshipRequests", new[] {"User_Id"});
            DropTable("dbo.FriendshipRequests");
        }
    }
}