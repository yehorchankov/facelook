#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class SecurityDBAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuthenticationTickets",
                c => new
                {
                    Id = c.Int(false, true),
                    UserId = c.Int(false),
                    PasswordHash = c.String(),
                    RegistrationDate = c.DateTime(false),
                    LastAccessTime = c.DateTime(false)
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.AuthenticationTickets");
        }
    }
}