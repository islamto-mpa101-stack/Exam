using Exam.Contexts;
using Exam.ViewModels.WorkViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class WorkController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _folderPath;


    public WorkController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        _folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");
    }

    public async Task<IActionResult> Index()
    {

        var Work = await _context.Works.Select( w => new WorkGetVM()
        {
            Id = w.Id,
            Name = w.Name,
            ImagePath = w.ImagePath,
            CategoryName = w.category.Name,


        }).ToListAsync();

        return View(Work);
    }




}

