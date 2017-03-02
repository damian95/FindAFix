using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindAFixMVC.Models.ViewModels
{
    public class JobViewModel
    {
        public int Id { get; set; }

        public string JobTitle { get; set; }

        public string JobDecription { get; set; }

        public string JobType { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }


    }
}