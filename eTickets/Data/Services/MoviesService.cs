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

        public async Task AddMovieAsync(MovieVM movieVM)
        {
            var movie = new Movie()
            {
                Name = movieVM.Name,
                Description = movieVM.Description,
                Price = movieVM.Price,
                ImageURL = movieVM.ImageURL,
                StartDate = movieVM.StartDate,
                EndDate = movieVM.EndDate,
                MovieCategory = movieVM.MovieCategory,
                CinemaId = movieVM.CinemaId,
                ProducerId = movieVM.ProducerId

            };

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            //Add movie Actors

            foreach (var actorId in movieVM.ActorIds)
            {
                var actorMovie = new Actor_Movie()
                {
                    MovieId = movie.Id,
                    ActorId = actorId
                };

                await _context.Actors_Movies.AddAsync(actorMovie);
            }

            await _context.SaveChangesAsync();
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

        public async Task UpdateMovieAsync(int id, MovieVM movieVM)
        {
            var movieDb = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieVM.Id);

            if (movieDb != null)
            {
                movieDb.Name = movieVM.Name;
                movieDb.Description = movieVM.Description;
                movieDb.Price = movieVM.Price;
                movieDb.ImageURL = movieVM.ImageURL;
                movieDb.StartDate = movieVM.StartDate;
                movieDb.EndDate = movieVM.EndDate;
                movieDb.MovieCategory = movieVM.MovieCategory;
                movieDb.CinemaId = movieVM.CinemaId;
                movieDb.ProducerId = movieVM.ProducerId;
                
                //_context.Movies.Update(movieDb);
                await _context.SaveChangesAsync();
            }

            //Remove Existing Actors 
            var existingActors = _context.Actors_Movies.Where(x => x.MovieId == movieDb.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActors);
            await _context.SaveChangesAsync();

            //Add movie Actors

            foreach (var actorId in movieVM.ActorIds)
            {
                var actorMovie = new Actor_Movie()
                {
                    MovieId = movieVM.Id,
                    ActorId = actorId
                };

                await _context.Actors_Movies.AddAsync(actorMovie);
            }

            await _context.SaveChangesAsync();
        }
    }
}
