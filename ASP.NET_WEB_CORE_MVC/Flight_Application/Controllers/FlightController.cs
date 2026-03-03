using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Flight_Application.Data;
using Flight_Application.Models;

namespace Flight_Application.Controllers
{
    public class FlightController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public FlightController(IConfiguration configuration)
        {
            _dbHelper = new DatabaseHelper(configuration);
        }

        public async Task<IActionResult> Index()
        {
            var model = new SearchViewModel();

            try
            {
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();

                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading data: {ex.Message}";
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlights(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }

            if (model.Source == model.Destination)
            {
                ModelState.AddModelError("", "Source and Destination cannot be the same");
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }

            try
            {
                var results = await _dbHelper.SearchFlightsAsync(model.Source, model.Destination, model.NumberOfPersons);
                ViewBag.SearchType = "Flights";
                ViewBag.SearchCriteria = $"{model.Source} to {model.Destination} for {model.NumberOfPersons} person(s)";
                return View("Results", results);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error searching flights: {ex.Message}";
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlightsWithHotels(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }

            if (model.Source == model.Destination)
            {
                ModelState.AddModelError("", "Source and Destination cannot be the same");
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }

            try
            {
                var results = await _dbHelper.SearchFlightsWithHotelsAsync(model.Source, model.Destination, model.NumberOfPersons);
                ViewBag.SearchType = "FlightsWithHotels";
                ViewBag.SearchCriteria = $"{model.Source} to {model.Destination} for {model.NumberOfPersons} person(s)";
                return View("Results", results);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error searching packages: {ex.Message}";
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }
        }
    }
}
