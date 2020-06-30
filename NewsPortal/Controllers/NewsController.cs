using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using EngContent;
using Microsoft.AspNet.Identity;
using NewsPortal.AppCache;
using NewsPortal.Managers;
using NewsPortal.Models;

namespace NewsPortal.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsRepository _news;
        private readonly ICommentRepository _comments;
        private CacheManager newsItemCache;
        private readonly IUnitOfWork _uow;

        public NewsController(IUnitOfWork unitOfWork,INewsRepository newsRepository, ICommentRepository commentRepository)
        {
            _uow = unitOfWork;
            _news = newsRepository;
            _comments = commentRepository;
            newsItemCache = new CacheManager();
        }

        [ValidateInput(false)]
        [AllowAnonymous]
        public ActionResult PreviewNewsItem(NewsItemViewModel model, HttpPostedFileBase picture)
        {
            if (!ModelState.IsValid)
                picture = null;
            model.ImageSrc = PreviewManager.PreviewPicture(model, picture);
            model.PublishDate = DateTime.Now;
            return View(model);
        }

        [AllowAnonymous]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Downstream)]
        public ActionResult Index(DateTime dateFilter, int currentPageNumber)
        {
            int pageCapacity = 6;
            if (currentPageNumber < 0)
                return HttpNotFound();
            var newsList = _news.GetSet(currentPageNumber, pageCapacity, dateFilter).ToList();
            var pageModel = new NewsPageViewModel
            {
                TotalPages = (_news.GetCountByDate(dateFilter) + pageCapacity - 1) / pageCapacity,
                PageNumber = currentPageNumber,
                NewsCurrentPage = newsList.Select(newsItem =>
                {
                    var itemModel = new NewsItemViewModel(newsItem);
                    itemModel.UrlToken = UrlManager.GenerateToken(itemModel.Title);
                    return itemModel;
                }).ToList()
            };

            return View(pageModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(NewsItemViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var newsItem = new NewsItem
            {
                Title = model.Title,
                Text = model.Text,
                PublishDate = DateTime.Now,
                UserId = User.Identity.GetUserId<int>()
            };
            NewsManager.SaveNewsPicture(newsItem, model.Picture);
            _news.Create(newsItem);
            _uow.Commit();
            newsItemCache.AddNewsItem(newsItem);
            HttpContext.Response.Cache.SetNoStore();
            return RedirectToAction("Index", "News");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var newsItem = _news.Get(id);

            if (newsItem.UserId != User.Identity.GetUserId<int>())
                return Json("Failed");
            NewsManager.DeleteNewsPicture(newsItem);
            var commentList = _comments.GetByNewsId(id);
            foreach (var item in commentList)
            {
                _comments.Delete(item.Id);
            }
            _news.Delete(id);
            newsItemCache.DeleteNewsItem(id);
            _uow.Commit();
            return Json("Success");
        }

        public ActionResult Edit(int id)
        {
            var newsItem = _news.Get(id);
            if (newsItem == null) return new HttpNotFoundResult();
            if (newsItem.UserId != User.Identity.GetUserId<int>())
                return new HttpUnauthorizedResult();

            return View(new NewsItemViewModel
            {
                Title = newsItem.Title,
                Text = newsItem.Text,
                ImageSrc = newsItem.ImageSrc,
                PublishDate = newsItem.PublishDate
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(NewsItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", ContentEng.NewsController_Edit_ModelError);
                return View(model);
            }

            if (_news.GetUserId(model.Id) != User.Identity.GetUserId<int>())
                return new HttpStatusCodeResult(403);

            var updatedItem = new NewsItem
            {
                Id = model.Id,
                Title = model.Title,
                Text = model.Text,
                ImageSrc = model.ImageSrc,
                PublishDate = model.PublishDate,
                UserId = User.Identity.GetUserId<int>()
            };

            NewsManager.UpdateNewsPicture(updatedItem, model.Picture, _news.Get(updatedItem.Id));
            _news.Update(updatedItem);
            newsItemCache.UpdateNewsItem(updatedItem);
            _uow.Commit();
            return RedirectToAction("Index", "News");
        }

        [AllowAnonymous]
        public ActionResult OpenNews(int id)
        {
            NewsItem newsItem;

            NewsItemViewModel model;

            var result = newsItemCache.GetNewsItem(id);

            if (result == null)
            {
                newsItem = _news.Get(id);
                newsItemCache.AddNewsItem(newsItem);
                if (newsItem == null)
                    return HttpNotFound();
                model = new NewsItemViewModel(newsItem);
            }
            else
            {
                model = new NewsItemViewModel(result);
            }
            return View(model);
        }
    }
}