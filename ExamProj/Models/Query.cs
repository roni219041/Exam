using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProj.Models
{
    public class Query
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Upload picture")]
        public string Image { get; set; }
        public string Status { get; set; }
        public DateTime? DateOfVisit { get; set; }
        public string TechName { get; set; }
        public Query()
        {
            Status = "waiting";
            DateOfVisit = null;
            TechName = "";
        }
     
    }
}
