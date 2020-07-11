

namespace GitHubRepositoriesSearchPage.Models
{
    public class Repository
    {
        public string id { get; set; }
        public string name { get; set; }
        public Owner owner { get; set; }
    }

    public class Owner
    {
        public string login { get; set; }
        public string avatar_url { get; set; }
    }
}


