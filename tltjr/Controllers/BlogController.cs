using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web;
using MongoDB.Bson;
using tltjr.Data;
using tltjr.Feed;
using tltjr.Models;

namespace tltjr.Controllers
{
    public class BlogController : Controller
    {
        private readonly PostRepository _postRepository = new PostRepository();
        private readonly RssHelper _rssHelper = new RssHelper();

        public ActionResult Index()
        {
            ViewBag.Title = "Blog";
            var posts = _postRepository.FindAll().ToList();
			posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            var indexModel = new IndexModel { Posts = posts.Take(10), SidebarModel = new SidebarModel(posts.Take(3)) };
            return View(indexModel);
        }

        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult New(Post post)
        {
            post.CreatedAt = DateTime.Now;
            if (post.TagsRaw != null)
            {
                var split = post.TagsRaw.Split(',');
                post.Tags = split.Select(o => o.Trim()).ToList();
            }
            _postRepository.Store(post);
            return RedirectToAction("Post", new {slug = post.Slug});
        }

        public ActionResult Post(string slug)
        {
            ViewBag.Title = "Blog";
            var post = _postRepository.FindOneByKey("Slug", slug);
            var posts = _postRepository.FindAll().ToList();
			posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            var postModel = new PostModel { Post = post, SidebarModel = new SidebarModel(posts.Take(3)) };
            return View(postModel);
        }

        public ActionResult Tag(string tag)
        {
            ViewBag.Title = "Posts Tagged: " + tag;
            var posts = _postRepository.FindAllByKey("Tags", tag);
            var indexModel = new IndexModel { Posts = posts.Take(10), SidebarModel = new SidebarModel(posts.Take(3)) };
            return View("Index", indexModel);
        }

        [Authorize]
        public ActionResult Edit(string objectId)
        {
            var post = _postRepository.FindOneById(objectId);
            EditId.Id = new ObjectId(objectId);
            return View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Post post)
        {
            post.CreatedAt = DateTime.Now;
            if (post.TagsRaw != null)
            {
                var split = post.TagsRaw.Split(',');
                post.Tags = split.Select(o => o.Trim()).ToList();
            }
            _postRepository.Update(post);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(string id)
        {
            _postRepository.DeleteById(new ObjectId(id));
            return RedirectToAction("Index");
        }

        public ActionResult Archive(DateTime dateTime)
        {
            ViewBag.Title = "Archive  - " + dateTime.ToString("MMMM") + " " + dateTime.Year;
            var posts = _postRepository.FindAll()
                .Where(o => o.CreatedAt.Month == dateTime.Month && o.CreatedAt.Year == dateTime.Year)
                .ToList();
            posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            var indexModel = new IndexModel { Posts = posts.Take(10), SidebarModel = new SidebarModel(posts.Take(3)) };
            return View("Index", indexModel);
        }

        public ActionResult Rss()
        {
            var posts = _postRepository.FindAll().ToList();
            posts.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
            var twenty = posts.Take(20);
            var title = ConfigurationManager.AppSettings["title"];
            var description = ConfigurationManager.AppSettings["description"];
            var uri = new Uri(ConfigurationManager.AppSettings["feeduri"]);
            var feed = new SyndicationFeed(title, description, uri,
                _rssHelper.CreateSyndicationItems(twenty, uri));
            return new RssActionResult { Feed = feed };
        }
    }
}