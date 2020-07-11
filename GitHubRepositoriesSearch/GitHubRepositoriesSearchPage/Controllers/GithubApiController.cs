using GitHubRepositoriesSearchPage.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace GitHubRepositoriesSearchPage.Controllers
{
    public class GithubApiController : Controller
    {
        public static IEnumerable<Repository> GetRepositories(string Keyword)
        {
            IEnumerable<Repository> RepositoriesList = new List<Repository>();
            string url = "https://api.github.com/search/repositories?q=" + Keyword;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "request");

                var response = httpClient.GetStringAsync(new Uri(url)).Result;

                JObject responseJO = JObject.Parse(response);

                // get JSON result objects into a list
                IList<JToken> results = responseJO["items"].Children().ToList();

                // serialize JSON results into .NET objects
                List<Repository> searchResults = new List<Repository>();
                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    Repository RepositoryResult = result.ToObject<Repository>();
                    searchResults.Add(RepositoryResult);
                }
                RepositoriesList = searchResults;
                return RepositoriesList;
            }
        }
    }
}
