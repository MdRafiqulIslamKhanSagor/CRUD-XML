using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notes.Models
{
    public class Service
    {

     

        
        public string id { get; set; }

        [Required(ErrorMessage = "Service Name required")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Image required")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string IsActive { get; set; }

        public Service()
        {
            DateTime date = DateTime.Now;
            id = String.Format(
                "{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}",
                date.Year, date.Month, date.Day,
                date.Hour, date.Minute, date.Second, date.Millisecond
            );
        }
    }
}