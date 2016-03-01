#region

using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace FaceLook.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("LoginRoute", "Login",
                new {controller = "Login", action = "Index"});

            routes.MapRoute("RegistrationRoute", "Registrate",
                new {controller = "Login", action = "Registrate"});

            routes.MapRoute("FriendsRequestRoute", "Friends/Requests",
                new {controller = "Friends", action = "FriendsToConfirm"});

            routes.MapRoute("AddFriendRoute", "Friends/Add",
                new {controller = "Friends", action = "Add"});

            routes.MapRoute("ConfirmFriendRoute", "Friends/Confirm",
                new {controller = "Friends", action = "Confirm"});

            routes.MapRoute("DenyFriendRoute", "Friends/Deny",
                new {controller = "Friends", action = "Deny"});

            routes.MapRoute("RemoveFriendRoute", "Friends/Remove",
                new {controller = "Friends", action = "Remove"});

            routes.MapRoute("FriendsRoute", "Friends/{login}",
                new {controller = "Friends", action = "Index", login = UrlParameter.Optional},
                new[] {"FaceLook.UI.Controllers"});

            routes.MapRoute("ConversationListRoute", "Conversation/List",
                new {controller = "Conversation", action = "List"});

            routes.MapRoute("ConversationRoute", "Conversation/{login}",
                new {controller = "Conversation", action = "Index", login = UrlParameter.Optional});

            routes.MapRoute("ConfirmationPageRoute", "Confirm/{login}/{key}",
                new {controller = "Login", action = "Confirm"});

            routes.MapRoute("PageItselfRoute", "Page/{login}",
                new {controller = "Account", action = "Page", login = UrlParameter.Optional},
                new[] {"FaceLook.UI.Controllers"});

            routes.MapRoute("PageWithHomeRoute", "Account/Page/{login}",
                new {controller = "Account", action = "Page", login = UrlParameter.Optional},
                new[] {"FaceLook.UI.Controllers"});

            routes.MapRoute("RedirectToLoginRoute", "Account/Login",
                new {controller = "Login", action = "Index"});

            routes.MapRoute("PageDefaultRoute", "{controller}/{action}/{login}",
                new {controller = "Account", action = "Page", login = UrlParameter.Optional},
                new[] {"FaceLook.UI.Controllers"}
                );
        }
    }
}