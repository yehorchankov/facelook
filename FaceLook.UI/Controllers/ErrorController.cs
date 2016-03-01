#region

using System.Web.Mvc;
using FaceLook.UI.Models;

#endregion

namespace FaceLook.UI.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PageNotFoundResult(string message)
        {
            return View((object) message);
        }

        public ActionResult HttpCodeResult(int code, string message)
        {
            RequestStatus status = new RequestStatus
            {
                Message = message,
                StatusCode = code
            };

            return View(status);
        }
    }
}