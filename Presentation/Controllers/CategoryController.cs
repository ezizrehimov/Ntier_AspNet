using Business.Services.Abstract;
using Business.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            var model = await service.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM model)
        {
            var result = await service.CreateAsync(model);
            if (!result)
                return View(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
