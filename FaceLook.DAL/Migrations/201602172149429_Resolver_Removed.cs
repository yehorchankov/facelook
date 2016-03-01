#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class Resolver_Removed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "ConflictResolverId", "dbo.ManyToManyResolvers");
            DropIndex("dbo.Users", new[] {"ConflictResolverId"});
            DropColumn("dbo.Users", "ConflictResolverId");
            DropTable("dbo.ManyToManyResolvers");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.ManyToManyResolvers",
                c => new
                {
                    Id = c.Int(false, true)
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Users", "ConflictResolverId", c => c.Int(false));
            CreateIndex("dbo.Users", "ConflictResolverId");
            AddForeignKey("dbo.Users", "ConflictResolverId", "dbo.ManyToManyResolvers", "Id", true);
        }
    }
}