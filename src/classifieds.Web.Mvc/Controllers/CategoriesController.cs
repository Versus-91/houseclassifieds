using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Categories;
using classifieds.Categories.Dto;
using classifieds.Web.Models.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace classifieds.Web.Controllers
{
    public class CategoriesController : AbpController
    {
        private readonly ICategoriesAppService _categoryService;
        public CategoriesController(ICategoriesAppService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: CategoriesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category =await _categoryService.GetAsync(new EntityDto<int> { Id = id});
            return View(category);
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryDto inputs)
        {
          await  _categoryService.CreateAsync(inputs);
          return RedirectToAction(nameof(Index));
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CategoriesController/Delete/5
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
