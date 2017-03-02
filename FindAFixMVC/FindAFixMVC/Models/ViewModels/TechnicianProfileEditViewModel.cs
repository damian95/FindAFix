using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindAFixMVC.Models.ViewModels
{
    public class TechnicianProfileEditViewModel
    {
        public int Id { get; set; }

        public int techId { get; set; }

        public string Description { get; set; }

        public string Rating { get; set; }

        public HttpPostedFileBase TechPicture { get; set; }
    }
}