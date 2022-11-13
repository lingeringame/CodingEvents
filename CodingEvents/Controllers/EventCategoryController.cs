using Microsoft.AspNetCore.Mvc;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;

namespace CodingEvents.Controllers
{
    public class EventCategoryController : Controller
    {
        public EventDbContext context;

        public EventCategoryController(EventDbContext dbContext) { this.context = dbContext; }

        //GET /<controller>/
        public IActionResult Index()
        {
            ViewBag.title = "All Categories";
            List<EventCategory> categories = context.Categories.ToList();
            return View(categories);
        }

        //GET /<controller>/create
        public IActionResult Create()
        {
            return View(new AddEventCategoryViewModel());
        }

        //POST /<controller>/
        [HttpPost("/create")]
        public IActionResult ProcessCreateEventCategoryForm(AddEventCategoryViewModel addEventCategoryViewModel)
        {
            if(ModelState.IsValid)
            {
                EventCategory newEventCategory = new EventCategory();
                newEventCategory.Name = addEventCategoryViewModel.Name;
                context.Categories.Add(newEventCategory);
                context.SaveChanges();
                return Redirect("/eventcategory");
            }
            return Redirect("/eventcategory/create");
        }
    }
}
