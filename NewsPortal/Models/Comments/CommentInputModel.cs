using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using EngContent;

namespace NewsPortal.Models
{
    public class CommentInputModel
    {
        [StringLength(maximumLength: 350, ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_CommentInput_Text_Length")]
        [Required(ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_CommentInput_Text_Required")]
        public string Text { get; set; }

        [Required]
        public int NewsId { get; set; }
    }
}