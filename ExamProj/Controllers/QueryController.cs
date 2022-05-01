using ExamProj.Data;
using ExamProj.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ExamProj.Controllers
{
    public class QueryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public QueryController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        // GET: QueryController
        public ActionResult Index(int? page, int? pageSize, string searchString)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "1", Value = "1" });
            items.Add(new SelectListItem { Text = "10", Value = "10", Selected = true });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });

            // all flights
            var queries = (from f in _context.Queries select f).ToList();

            // search
            ViewData["CurrentFilter"] = searchString;
            if (!string.IsNullOrEmpty(searchString))
            {
                queries = queries.Where(f => f.Title.Contains(searchString)
                                       || f.TechName.Contains(searchString)).ToList();
            }

            pageSize = pageSize ?? 10;

            var model = new QueryViewModel()
            {
                PageSize = pageSize ?? 10,
                PageSizeList = items,
                Queries = queries.ToPagedList(page ?? 1, pageSize.Value)
            };

            return View(model);

        }

        // GET: QueryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: QueryController/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: QueryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Title, Description, Address, Image, Status, DateOfVisit, Techname")] CreateQueryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Query query = new Query
                {
                    Title = model.Title,
                    Description = model.Description,
                    Address = model.Address,
                    Image = uniqueFileName,
                    Status = "waiting",
                };
                _context.Add(query);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        //Image upload
        private string UploadedFile(CreateQueryViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        // GET: QueryController/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            var serviceFromDb = _context.Queries.Find(id);
            return View();
        }

        // POST: QueryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Query query)
        {
            if (!ModelState.IsValid)
            {
                _context.Queries.Update(query);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // GET: QueryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QueryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
