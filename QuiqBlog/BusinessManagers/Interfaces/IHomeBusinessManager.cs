using Microsoft.AspNetCore.Mvc;
using QuiqBlog.Models.HomeViewModels;
using System.Threading.Tasks;

namespace QuiqBlog.BusinessManagers.Interfaces {
    public interface IHomeBusinessManager {
        ActionResult<AuthorViewModel> GetAuthorViewModel(string authorId, string searchString, int? page);
    }
}