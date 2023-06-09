﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using QuiqBlog.BusinessManagers.Interfaces;
using QuiqBlog.Data;
using QuiqBlog.Data.Migrations;
using QuiqBlog.Data.Models;
using QuiqBlog.Models.PostViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuiqBlog.Controllers {
    [Authorize]
    public class PostController : Controller {

        private readonly IPostBusinessManager postBusinessManager;
     
        public PostController(IPostBusinessManager postBusinessManager) {
            this.postBusinessManager = postBusinessManager;
        }

        [Route("Post/{id}"), AllowAnonymous]
        public async Task<IActionResult> Index(int? id) {
            var actionResult = await postBusinessManager.GetPostViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        public IActionResult Create() {
            var model = new CreateViewModel
            {
                Categories = postBusinessManager.GetCategories(),
                PostTypes = postBusinessManager.GetPostTypes()
            };
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id) {
            var actionResult = await postBusinessManager.GetEditViewModel(id, User);

            if (actionResult.Result is null)
                return View(actionResult.Value);

            return actionResult.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateViewModel createViewModel) {
            await postBusinessManager.CreatePost(createViewModel, User);
            return RedirectToAction("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel editViewModel) {
            var actionResult = await postBusinessManager.UpdatePost(editViewModel, User);
       

            if (actionResult.Result is null)
                return RedirectToAction("Edit", new { editViewModel.Post.Id });

            return actionResult.Result;
        }
       
        
        [HttpPost]
        public async Task<IActionResult> Comment(PostViewModel postViewModel) {
            var actionResult = await postBusinessManager.CreateComment(postViewModel, User);

            if (actionResult.Result is null)
                return RedirectToAction("Index", new { postViewModel.Post.Id });

            return actionResult.Result;
        }

    }
}