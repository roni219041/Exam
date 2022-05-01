using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProj.Models
{
    public class CreateQueryViewModel
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
        [NotMapped]
        [Display(Name = "Upload picture")]
        public IFormFile Image { get; set; }
    }
}
