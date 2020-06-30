using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPortal.Models
{
    public class NewsPageViewModel
    {
        public List<NewsItemViewModel> NewsCurrentPage { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages { get; set; }
    }
}