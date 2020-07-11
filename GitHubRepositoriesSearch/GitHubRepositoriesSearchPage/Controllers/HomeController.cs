using GitHubRepositoriesSearchPage.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GitHubRepositoriesSearchPage.Controllers
{
    public class HomeController : Controller
    {
        static IEnumerable<Repository> repositoriesList = new List<Repository>();
        static List<Repository> bookmarkList = new List<Repository>();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string search)
        {
            repositoriesList = GithubApiController.GetRepositories(search);
            return RedirectToAction("Result", "Home");
        }

        public ActionResult Result()
        {
            return View(repositoriesList);
        }

        public ActionResult Bookmarks()
        {
            return View(bookmarkList);
        }


        public ActionResult AddToBookmarks(string id)
        {
            Repository bookmark = repositoriesList.Where(r => r.id == id).FirstOrDefault();
            Session.Add(id, bookmark);

            foreach (string item in Session.Contents)
            {
                Repository getbookmark = (Repository)Session[item];

                if (bookmarkList.Count == 0)
                {
                    bookmarkList.Add(getbookmark);
                }

                 if (bookmarkList.Where(r => r.id == getbookmark.id).FirstOrDefault() == null )
                {
                    bookmarkList.Add(getbookmark);
                }
            }
            return RedirectToAction("Bookmarks", "Home");
        }
    }
}