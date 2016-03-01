#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class Conversations_Model_Changed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Conversations", "User_Id", "dbo.Users");
            DropIndex("dbo.Conversations", new[] {"User_Id"});
            DropColumn("dbo.Conversations", "User_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.Conversations", "User_Id", c => c.Int());
            CreateIndex("dbo.Conversations", "User_Id");
            AddForeignKey("dbo.Conversations", "User_Id", "dbo.Users", "Id");
        }
    }
}