using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MoviesService :EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;

        public MoviesService(AppDbContext context) :base(context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Producer)
                .Include(m => m.Actors_Movies).ThenInclude(a=> a.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);

            return movie;
        }

        public async Task<MovieDropdownsVM> GetMovieDropdownsValues()
        {
            var response = new MovieDropdownsVM
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }
    }
}
