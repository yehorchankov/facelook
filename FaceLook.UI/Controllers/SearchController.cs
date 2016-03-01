#region

using System.Web.Mvc;
using FaceLook.BL.Concrete;
using FaceLook.DAL.Abstract;
using FaceLook.UI.Models;

#endregion

namespace FaceLook.UI.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly Filters _searchFilters;
        private readonly IUserRepository _userRepository;

        public SearchController(Filters filters, IUserRepository userRepository)
        {
            _searchFilters = filters;
            _userRepository = userRepository;
        }

        // GET: Search
        public ActionResult Index(string search)
        {
            SearchResult result = new SearchResult
            {
                Posts = _searchFilters.FindPostsByTag(search),
                Users = _searchFilters.FindUsersByName(search),
                CurrentUser = _userRepository.GetUserByNickname(User.Identity.Name)
            };

            return View(result);
        }
    }
}