#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class DB_Dropped : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "FriendsToConfirm_Id", "dbo.ManyToManyResolvers");
            DropIndex("dbo.Users", new[] {"FriendsToConfirm_Id"});
            RenameColumn("dbo.Users", "FriendsToConfirm_Id", "ConflictResolverId");
            AlterColumn("dbo.Users", "ConflictResolverId", c => c.Int(false));
            CreateIndex("dbo.Users", "ConflictResolverId");
            AddForeignKey("dbo.Users", "ConflictResolverId", "dbo.ManyToManyResolvers", "Id", true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Users", "ConflictResolverId", "dbo.ManyToManyResolvers");
            DropIndex("dbo.Users", new[] {"ConflictResolverId"});
            AlterColumn("dbo.Users", "ConflictResolverId", c => c.Int());
            RenameColumn("dbo.Users", "ConflictResolverId", "FriendsToConfirm_Id");
            CreateIndex("dbo.Users", "FriendsToConfirm_Id");
            AddForeignKey("dbo.Users", "FriendsToConfirm_Id", "dbo.ManyToManyResolvers", "Id");
        }
    }
}