using Microsoft.AspNetCore.Mvc;
using Sticks.Web.Data;
using Sticks.Web.Models.Domain;
using Sticks.Web.Models.ViewModels;

namespace Sticks.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly SticksDbContext sticksDbContext;

        public AdminTagsController(SticksDbContext sticksDbContext)
        {
            this.sticksDbContext = sticksDbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
            //Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            sticksDbContext.Tags.Add(tag);
            sticksDbContext.SaveChanges();

            return View("Ädd");
        }
    }
}
