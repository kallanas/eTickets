using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
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

        //SearchBar
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var filteredResult = allMovies.Where(x => x.Name.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower())).ToList();
                
                if (filteredResult.Any())
                {
                    return View("Index", filteredResult);
                }
            }

            return View("Index", allMovies);
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

        [HttpPost]
        public async Task<IActionResult> Create(MovieVM movieVM)
        {
            if (!ModelState.IsValid)
            {
                var moviesDropdownData = await _service.GetMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(moviesDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(moviesDropdownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(moviesDropdownData.Actors, "Id", "FullName");

                return View(movieVM);
            }

            await _service.AddMovieAsync(movieVM);

            return RedirectToAction(nameof(Index));
        }

        //Update
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if (movie == null) return View("NotFound");

            var response = new MovieVM()
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageURL = movie.ImageURL,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                CinemaId = movie.CinemaId,
                ProducerId = movie.ProducerId,
                ActorIds = movie.Actors_Movies.Select(m => m.ActorId).ToList()
            };

            var moviesDropdownData = await _service.GetMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(moviesDropdownData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(moviesDropdownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(moviesDropdownData.Actors, "Id", "FullName");

            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, MovieVM movieVM)
        {
            if (id != movieVM.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var moviesDropdownData = await _service.GetMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(moviesDropdownData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(moviesDropdownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(moviesDropdownData.Actors, "Id", "FullName");

                return View(movieVM);
            }

            await _service.UpdateMovieAsync(id, movieVM);
            return RedirectToAction(nameof(Index));
        }
    }
}
