using Microsoft.AspNetCore.Mvc;
using CodingEvents.Models;
using CodingEvents.Data;
using CodingEvents.ViewModels;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = new List<Event>(EventData.GetAll());

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
                    ContactEmail = addEventViewModel.ContactEmail
                };
                EventData.Add(newEvent);

                return Redirect("/events");
            }
            return View(addEventViewModel);
        }

        // GET: /<controller>/delete
        public IActionResult Delete()
        {
            ViewBag.events = EventData.GetAll();
            return View();
        }

        // POST: /<controller>/delete
        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach(int eventId in eventIds)
            {
                EventData.Remove(eventId);
            }
            return Redirect("/events");
        }

        //GET : /<controller>/edit
        [HttpGet]
        [Route("/events/edit/{eventId}")]
        public IActionResult Edit(int eventId)
        {
            ViewBag.Event = EventData.GetById(eventId);
            ViewBag.title = $"Edit Event {ViewBag.Event.Name}(id={ViewBag.Event.Id})";
            return View();
        }

        // POST: /<controller>/edit
        [HttpPost("/events/edit")]
        [Route("/events/edit/{eventId}")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description)
        {
            Event eventToBeEdited = EventData.GetById(eventId);
            eventToBeEdited.Name = name;
            eventToBeEdited.Description = description;
            return Redirect("/events");
        }
    }
}
