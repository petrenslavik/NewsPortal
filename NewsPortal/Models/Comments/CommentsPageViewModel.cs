using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPortal.Models
{
    public class CommentsPageViewModel
    {
        public List<CommentViewModel> CommentsCurrentPage { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages { get; set; }
    }
}