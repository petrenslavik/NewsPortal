using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Models
{
    public class CommentViewModel
    { 
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Text { get; set; }

        public DateTime PublishDate { get; set; }

        public int Id { get; set; }

        public int UserId { get; set; }
    }
}