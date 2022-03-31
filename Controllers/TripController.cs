using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project1.Models;
using System.Linq;

namespace Project1.Controllers
{
    public class TripController : Controller
    {

        private TripContext context { get; set; }

        public TripController(TripContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var trips = context.Trips.ToList();

            return View(trips);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Trip trip)
        {
            if (ModelState.IsValid)
            {
                TempData["trip"] = JsonConvert.SerializeObject(trip);

                return RedirectToAction(nameof(AddTripLog));
            }
            return View(nameof(Add));
        }

        [HttpGet]
        public IActionResult AddTripLog()
        {
            if (TempData["trip"] != null)
            {
                Trip oldTrip = JsonConvert.DeserializeObject<Trip>(TempData["trip"].ToString());
                ViewBag.Accommodation = oldTrip.Accommodation;
                TempData["trip"] = JsonConvert.SerializeObject(oldTrip);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddTripLog(Trip trip)
        {
            if (TempData["trip"] != null)
            {
                Trip oldTrip = JsonConvert.DeserializeObject<Trip>(TempData["trip"].ToString());

                oldTrip.AccommodationPhone = trip.AccommodationPhone;
                oldTrip.AccommodationEmail = trip.AccommodationEmail;

                TempData["AddTrip"] = JsonConvert.SerializeObject(oldTrip);

                return RedirectToAction(nameof(AddThingsToDo));
            }
            return View(nameof(AddTripLog));
        }
        [HttpGet]
        public IActionResult AddThingsToDo()
        {
            if (TempData["AddTrip"] != null)
            {
                Trip oldTrip = JsonConvert.DeserializeObject<Trip>(TempData["AddTrip"].ToString());
                ViewBag.Accommodation = oldTrip.Accommodation;

                TempData["AddTrip"] = JsonConvert.SerializeObject(oldTrip);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddThingsToDo(Trip trip)
        {
            if (TempData["AddTrip"] != null)
            { 
                Trip oldTrip = JsonConvert.DeserializeObject<Trip>(TempData["AddTrip"].ToString());
                oldTrip.ThingToDo1 = trip.ThingToDo1;
                oldTrip.ThingToDo2 = trip.ThingToDo2;
                oldTrip.ThingToDo3 = trip.ThingToDo3;

                //add to db
                context.Trips.Add(oldTrip);

                context.SaveChanges();
                ViewBag.Message = $"Trip to {oldTrip.Destination} added successfully.";

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(AddThingsToDo));
        }

        public IActionResult Cancel()
        {
            TempData.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}

