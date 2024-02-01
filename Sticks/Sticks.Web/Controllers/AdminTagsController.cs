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

            return RedirectToAction("List");
        }


        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            //use dbContext to read the tags
            var tags = sticksDbContext.Tags.ToList();


            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = sticksDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }


        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = sticksDbContext.Tags.Find(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //save changes
                sticksDbContext.SaveChanges();

                //show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id }); ;
            }

            //show error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }


        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var tag = sticksDbContext.Tags.Find(editTagRequest.Id);

            if (tag != null)
            {
                sticksDbContext.Tags.Remove(tag);
                sticksDbContext.SaveChanges();

                //show a success notification
                return RedirectToAction("List");
            }

            //show an error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id }); ;
        }
    }
}
