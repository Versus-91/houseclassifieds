using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Cities;
using classifieds.Cities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace classifieds.Web.Controllers
{
    public class CitiesController : AbpController
    {
        private readonly ICitiesAppService _cityService;
        public CitiesController(ICitiesAppService cityService)
        {
            _cityService = cityService;
        }
        // GET: CategoriesController
        public async Task<ActionResult> Index(int pageindex = 1, string sort = "Name")
        {
            var categories = (await _cityService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 100 })).Items;
            var model = PagingList.Create(categories, 10, pageindex, sort, "Name");
            return View(model);
        }

        // GET: CategoriesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var category = await _cityService.GetAsync(new EntityDto<int> { Id = id });
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
        public async Task<ActionResult> Create(CityDto inputs)
        {
            await _cityService.CreateAsync(inputs);
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
