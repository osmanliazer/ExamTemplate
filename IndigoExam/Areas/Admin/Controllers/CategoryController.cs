using IndigoExam.DAL;
using IndigoExam.Models;
using IndigoExam.ViewModels.Categorys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IndigoExam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IndigoContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryController(IndigoContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        public async Task<IActionResult> Create()
        {
            CreateCategoryVM categoryVM = new CreateCategoryVM();
            {
                List<Category> Categories = await _context.Categories.ToListAsync();
            }
            return View(categoryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {

                categoryVM.Categories = await _context.Categories.ToListAsync();
                return View(categoryVM);
            }

            string filename = Guid.NewGuid().ToString() + categoryVM.Img.FileName;

            string path = Path.Combine(_env.WebRootPath, "admin", "images", filename);
            using (FileStream file = new FileStream(path, FileMode.Create))
            {

                await categoryVM.Img.CopyToAsync(file);

            }
            await _context.Categories.AddAsync(new Category
            {

                Img = filename,
                Name = categoryVM.Name,
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            Category existed = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            string path = Path.Combine(_env.WebRootPath, "admin", "images", existed.Img);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.Categories.Remove(existed);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            Category existed = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            UpdateCategoryVM updateCategoryVM = new UpdateCategoryVM
            {
                Name = existed.Name,
                Image = existed.Img,

            };


            return View(updateCategoryVM);
        }
        [HttpPost]

        public async Task<IActionResult> Update(int id, UpdateCategoryVM updateCategoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(updateCategoryVM);
            }
            Category existed = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            if (updateCategoryVM.Photo != null)
            {
                string filename = Guid.NewGuid().ToString() + updateCategoryVM.Photo.FileName;

                string path = Path.Combine(_env.WebRootPath, "admin", "images",  filename);
                using (FileStream file = new FileStream(path, FileMode.Create))
                {

                    await updateCategoryVM.Photo.CopyToAsync(file);

                }
                existed.Img = filename;
            }
            existed.Name = updateCategoryVM.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}