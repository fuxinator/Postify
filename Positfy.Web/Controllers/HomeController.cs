using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Positfy.Data.ViewModels;
using Positfy.Web.Models;
using Postify.Data.Data;
using Postify.Data.Models;
using Postify.Data.ViewModels;

namespace Postify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> CreatePost(CreatePostViewModel vm)
        {
            var currentUser = await GetCurrentUserAsync();

            var newPost = new Post
            {
                User = currentUser,
                Content = vm.Content,
                Published = DateTime.Now
            };

            _context.Add(newPost);
            await _context.SaveChangesAsync();

            var posts = await SortPosts();

            posts = posts.Skip(vm.Index * 20).Take(20).ToList();

            List<PostViewModel> postsViewModels = CreatePostViewModel(posts, currentUser);

            return Json(postsViewModels);
        }

        

        public async Task<ActionResult> CreateComment(CreateCommentViewModel vm)
        {
            var currentUser = await GetCurrentUserAsync();

            if (!User.Identity.IsAuthenticated)
                return Json("");

            if (vm.Content.Length > 300)
                vm.Content = vm.Content.Substring(0, 300);

            var posts = await SortPosts();
            var post = posts[vm.PostId];

            post.Comments.Add(new Comment { Content = vm.Content, User = currentUser, Published = DateTime.Now });
            _context.Update(post);
            await _context.SaveChangesAsync();

            var commentViewModels = new List<CommentViewModel>();

            foreach (var comment in post.Comments)
            {
                commentViewModels.Add(new CommentViewModel()
                {
                    User = comment.User.UserName,
                    Content = comment.Content,
                    Published = comment.Published.ToString("g"),
                    LikeCount = comment.Likes.Count,
                    IsLikeable = comment.Likes.All(p => p.User != currentUser) && User.Identity.IsAuthenticated
                });
            }

            return Json(commentViewModels);
        }

        private async Task<List<Post>> SortPosts()
        {
            var posts = await _context.Posts
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Likes)
                .Include(p => p.User)
                .Include(p => p.Likes)
                .ToListAsync();

            posts.Sort();

            return posts.ToList();
        }

        private List<PostViewModel> CreatePostViewModel(List<Post> posts, ApplicationUser currentUser)
        {
            var models = new List<PostViewModel>();
            foreach (Post post in posts)
            {
                PostViewModel postVM = new PostViewModel()
                {
                    Owner = post.User.UserName,
                    Content = post.Content,
                    PostDate = post.Published.ToString("g"),
                    LikeCount = post.Likes.Count,
                    Id = post.PostId,
                    IsLikeable = post.Likes.All(p => p.User != currentUser) && User.Identity.IsAuthenticated,
                    Comments = new List<CommentViewModel>()
                };

                foreach (Comment comment in post.Comments)
                {
                    postVM.Comments.Add(new CommentViewModel()
                    {
                        User = comment.User.UserName,
                        Content = comment.Content,
                        Published = comment.Published.ToString("g"),
                        LikeCount = comment.Likes.Count,
                        IsLikeable = comment.Likes.All(c => c.User != currentUser) && User.Identity.IsAuthenticated
                    });
                }

                models.Add(postVM);
            }

            return models;
        }

        public async Task<IActionResult> LoadMore(int loadMoreCounter)
        {
            var currentUser = await GetCurrentUserAsync();

            var posts = await SortPosts();

            posts = posts.Skip(loadMoreCounter * 20).Take(20).ToList();

            List<PostViewModel> models = CreatePostViewModel(posts, currentUser);

            return Json(models);
        }

        public async Task<ActionResult> LikePost(int postId)
        {
            var currentUser = await GetCurrentUserAsync();

            var posts = await SortPosts();
            var post = posts[postId];
            
            if (post.Likes.Any(l => l.User == currentUser))
            {
                return Json(post.Likes.Count);
            }
            else
            {
                post.Likes.Add(new PostLike { Post = post, User = currentUser });
                _context.Update(post);
                await _context.SaveChangesAsync();

                return Json(post.Likes.Count);
            }   
        }

        public async Task<ActionResult> LikeComment(CommentLikeViewModel vm)
        {
            var currentUser = await GetCurrentUserAsync();

            var posts = await SortPosts();
            var post = posts[vm.PostId];

            var comment = post.Comments.ToList()[vm.CommentId];
            
            if (comment.Likes.Any(l => l.User == currentUser))
                return Json(comment.Likes.Count);

            comment.Likes.Add(new CommentLike { Comment = comment, User = currentUser });
            _context.Update(comment);
            await _context.SaveChangesAsync();

            return Json(comment.Likes.Count);
        }
        
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
