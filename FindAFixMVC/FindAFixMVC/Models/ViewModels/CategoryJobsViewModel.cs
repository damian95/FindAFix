using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindAFixMVC.Models.ViewModels
{
    public class CategoryJobsViewModel
    {
        public int Id { get; set; }

        public int techid { get; set; }
        public string techAspnetId { get; set; }

        public string CompanyName { get; set; }

        public string Rating { get; set; }
    }
}