using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using AM.UI.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AM.UI.Web.Controllers
{
    public class FlightController : Controller
    {
        IUnitOfWork unitOfWork;
        IFlightService flightService;
        IWebHostEnvironment webHostEnvironment;
        IPlaneService planeService;
        public FlightController(IUnitOfWork unitOfWork, IFlightService flightService, IWebHostEnvironment webHostEnvironment, IPlaneService planeService)
        {
            this.unitOfWork = unitOfWork;
            this.flightService = flightService;
            this.webHostEnvironment = webHostEnvironment;
            this.planeService = planeService;
        }
        // GET: FlightController
        public ActionResult Index()
        {
            return View(flightService.GetAll());
        }

        [HttpPost]
        public ActionResult Index(DateTime? flightDate)
        {
            return View(flightDate == null ? flightService.GetAll() : flightService.GetFlightsGreaterThan(flightDate.Value));
        }

        // GET: FlightController/Details/5
        public ActionResult Details(int id)
        {
            return View(flightService.GetById(id));
        }

        // GET: FlightController/Create
        public ActionResult Create()
        {
            ViewBag.Planes = new SelectList(planeService.GetAll().Select(e => new {e.PlaneId, Description=$"PlaneId : {e.PlaneId}, Capacity = {e.Capacity}"}), nameof(Plane.PlaneId), "Description"); 
            return View();
        }

        // POST: FlightController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Flight flight)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var fileName = Request.AddRequestFile(webHostEnvironment, "wwwroot", "upload");

                flight.Pilot = fileName;
                flightService.Add(flight);
                unitOfWork.Commit();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(flightService.GetById(id));
        }

        // POST: FlightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Flight flight)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                flightService.Update(flight);
                unitOfWork.Commit();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FlightController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(flightService.GetById(id));
        }

        // POST: FlightController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Flight flight)
        {
            try
            {
                flight.FlightId = id;
                flightService.Delete(flight);
                unitOfWork.Commit();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
