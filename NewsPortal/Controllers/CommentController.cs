using System;
using System.Linq;
using System.Web.Mvc;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNet.Identity;
using NewsPortal.Models;
using EngContent;
using System.Web.UI;

namespace NewsPortal.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentsRepository;
        private readonly IUserRepository _usersRepository;
        private readonly IUnitOfWork _uow;

        public CommentController(IUnitOfWork unitOfWork, ICommentRepository commentsRepository, IUserRepository userRepository)
        {
            _uow = unitOfWork;
            _commentsRepository = commentsRepository;
            _usersRepository = userRepository;
        }

        public ActionResult Create(int id)
        {
            return PartialView(new CommentInputModel { NewsId = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", ContentEng.CommentController_Create_ModelError);
                return PartialView(model);
            }
            _commentsRepository.Create(new Comment
            {
                UserId = User.Identity.GetUserId<int>(),
                PublishDate = DateTime.Now,
                Text = model.Text,
                NewsId = model.NewsId
            });
            _uow.Commit();
            return RedirectToAction("OpenNews", "News", new { id = model.NewsId });
        }

        [AllowAnonymous]
        [OutputCache(Duration = 600, Location = OutputCacheLocation.Downstream)]
        public ActionResult ListOfComments(int newsId, int currentPageNumber)
        {
            int pageCapacity = 3;
            if (currentPageNumber < 0)
                return HttpNotFound();

            var comments = _commentsRepository.GetSetByNewsId(newsId, currentPageNumber, pageCapacity);

            var pageModel = new CommentsPageViewModel
            {
                TotalPages = (_commentsRepository.GetCountByNewsId(newsId) + pageCapacity - 1) / pageCapacity,
                PageNumber = currentPageNumber,
                CommentsCurrentPage = comments.Select(comment =>
                {
                    var model = new CommentViewModel
                    {
                        Text = comment.Text,
                        PublishDate = comment.PublishDate,
                        Id = comment.Id
                    };
                    var user = _usersRepository.Get(comment.UserId);
                    if (user == null)
                        return null;
                    model.Name = user.Name;
                    model.LastName = user.Surname;
                    model.UserId = user.Id;
                    return model;
                }).ToList()
            };
            return PartialView(pageModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int commentId)
        {
            var comment = _commentsRepository.Get(commentId);
            if (comment == null)
                return new EmptyResult();
            if (User.Identity.GetUserId<int>() != comment.UserId)
                return new HttpStatusCodeResult(403, "Permission denied.");

            int newsId = comment.NewsId;
            _commentsRepository.Delete(commentId);
            _uow.Commit();
            return RedirectToAction("OpenNews", "News", new { id = newsId });

        }
    }
}