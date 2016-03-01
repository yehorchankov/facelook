#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "NickName", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.Users", "NickName", c => c.String(false));
        }
    }
}