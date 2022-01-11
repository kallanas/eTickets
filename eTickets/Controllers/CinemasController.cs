using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        //Details
        public async Task<IActionResult> Details(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");

            return View(cinema);
        }

        //Update
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null) return View("NotFound");

            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);

            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        //Delete
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema is null) return View("NotFound");

            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema is null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
