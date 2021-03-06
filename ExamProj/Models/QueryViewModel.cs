using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ExamProj.Models
{
    public class QueryViewModel
    {
        [Display(Name = "Page size")]
        public int PageSize { get; set; }
        public IEnumerable<SelectListItem> PageSizeList { get; set; }
        public IPagedList<Query> Queries { get; set; }

    }
}
