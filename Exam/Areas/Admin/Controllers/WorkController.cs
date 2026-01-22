using Exam.Contexts;
using Exam.Models;
using Exam.ViewModels.WorkViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WorkController(AppDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.categories = _context.Categories.ToList();
                return View(vm);
            }

           
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.Image.FileName);
            string uploadPath = Path.Combine(_env.WebRootPath, "assets", "images", fileName);

            using (var fileStream = new FileStream(uploadPath, FileMode.Create))
            {
                await vm.Image.CopyToAsync(fileStream);
            }

            Work work = new()
            {
                Name = vm.Name,
                CategoryId = vm.CategoryId,
                ImagePath = fileName
            };

            await _context.Works.AddAsync(work);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null) return NotFound();

            ViewBag.categories = _context.Categories.ToList();

            WorkUpdateVM vm = new()
            {
                Id = work.Id,
                Name = work.Name,
                CategoryId = work.CategoryId
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, WorkUpdateVM vm)
        {
            if (id != vm.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.categories = _context.Categories.ToList();
                return View(vm);
            }

            var existWork = await _context.Works.FindAsync(id);
            if (existWork == null) return NotFound();

            if (vm.Image != null)
            {
                string oldPath = Path.Combine(_env.WebRootPath, "assets", "images", existWork.ImagePath);
                if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.Image.FileName);
                string newPath = Path.Combine(_env.WebRootPath, "assets", "images", fileName);

                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    await vm.Image.CopyToAsync(fileStream);
                }
                existWork.ImagePath = fileName;
            }

            existWork.Name = vm.Name;
            existWork.CategoryId = vm.CategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work == null) return NotFound();

            string imagePath = Path.Combine(_env.WebRootPath, "assets", "images", work.ImagePath);
            if (System.IO.File.Exists(imagePath)) System.IO.File.Delete(imagePath);

            _context.Works.Remove(work);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
