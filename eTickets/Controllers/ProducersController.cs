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
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if (producer == null) return View("NotFound");

            return View(producer);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }


        //Update
        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);

            if(id == producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }

            return View(producer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if (producer == null) return View("NotFound");

            return View(producer);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = await _service.GetByIdAsync(id);
            if (producer == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
