using PagedList.Core;
using QuiqBlog.Data.Models;

namespace QuiqBlog.Models.HomeViewModels {
    public class AuthorViewModel {
        public ApplicationUser Author { get; set; }
        public IPagedList<Post> Posts { get; set; }
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
        public int FollowersCount { get; set; }
        public int SubscriptionsCount { get; set; }
    }
}