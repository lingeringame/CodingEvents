using Microsoft.AspNetCore.Mvc;
using CodingEvents.Models;
using CodingEvents.Data;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.CookiePolicy;
using System.Linq;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        private EventDbContext context;

        public EventsController(EventDbContext dbContext)
        {
            this.context = dbContext;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = context.Events.ToList();

            return View(events);
        }

        // GET: /<controller>/add
        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();

            return View(addEventViewModel);
        }

        //POST : /<controller>/add
        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if(ModelState.IsValid)
            {
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Type = addEventViewModel.Type
                };
                context.Events.Add(newEvent);
                context.SaveChanges();

                return Redirect("/events");
            }
            return View(addEventViewModel);
        }

        // GET: /<controller>/delete
        public IActionResult Delete()
        {
            ViewBag.events = context.Events.ToList();
            return View();
        }

        // POST: /<controller>/delete
        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach(int eventId in eventIds)
            {
                Event theEvent = context.Events.Find(eventId);
                context.Events.Remove(theEvent);
            }
            context.SaveChanges();
            return Redirect("/events");
        }

        //GET : /<controller>/edit
        [HttpGet]
        [Route("/events/edit/{eventId}")]
        public IActionResult Edit(int eventId)
        {
            ViewBag.Event = context.Events.Find(eventId);
            //ViewBag.Event = EventData.GetById(eventId);
            ViewBag.title = $"Edit Event {ViewBag.Event.Name}(id={ViewBag.Event.Id})";
            return View();
        }

        // POST: /<controller>/edit
        [HttpPost("/events/edit")]
        [Route("/events/edit/{eventId}")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description)
        {
            Event eventToBeEdited = context.Events.Find(eventId);
            eventToBeEdited.Name = name;
            eventToBeEdited.Description = description;
            context.SaveChanges();
            return Redirect("/events");
        }
    }
}
