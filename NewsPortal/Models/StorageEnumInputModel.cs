using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsPortal.Managers;
using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Models
{
    public class ToggleStorageInputModel
    {
        [Required]
        public Storage Storage { get; set; }
    }
}