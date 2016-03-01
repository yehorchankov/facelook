#region

using System.Data.Entity.Migrations;

#endregion

namespace FaceLook.DAL.Migrations
{
    public partial class SecurityChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuthenticationTickets", "UserNickname", c => c.String());
            AddColumn("dbo.AuthenticationTickets", "TicketKey", c => c.Guid(false));
            AlterColumn("dbo.Users", "NickName", c => c.String(false));
            DropColumn("dbo.AuthenticationTickets", "UserId");
        }

        public override void Down()
        {
            AddColumn("dbo.AuthenticationTickets", "UserId", c => c.Int(false));
            AlterColumn("dbo.Users", "NickName", c => c.String());
            DropColumn("dbo.AuthenticationTickets", "TicketKey");
            DropColumn("dbo.AuthenticationTickets", "UserNickname");
        }
    }
}