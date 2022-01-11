using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies =await _service.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if (movie is null) return View("NotFound");

            return View(movie);
        }

        //Create
        public async Task<IActionResult> Create()
        {
            var moviesDropdownData = await _service.GetMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(moviesDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(moviesDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(moviesDropdownData.Actors, "Id", "FullName");

            return View();
        }
    }
}
