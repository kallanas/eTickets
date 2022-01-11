using eTickets.Data;
using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class MovieVM
    {
        [Required(ErrorMessage = "The name is required.")]
        [Display(Name = "Movie Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The description is required.")]
        [Display(Name = "Movie Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The price is required.")]
        [Display(Name = "Movie Price")]
        public double Price { get; set; }
        [Required(ErrorMessage = "The Movie poster URL is required.")]
        [Display(Name = "Movie poster URL")]
        public string ImageURL { get; set; }
        [Required(ErrorMessage = "The start date is required.")]
        [Display(Name = "Start Date of Movie")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "The end date is required.")]
        [Display(Name = "End Date of Movie")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "The movie category is required.")]
        [Display(Name = "Select a category")]
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        [Required(ErrorMessage = "At least one actor is required.")]
        [Display(Name = "Select actor(s)")]
        public List<int> ActorIds { get; set; }
        [Required(ErrorMessage = "Cinema is required.")]
        [Display(Name = "Select a Cinema")]
        public int CinemaId { get; set; }
        [Required(ErrorMessage = "Producer is required.")]
        [Display(Name = "Select a Producer")]
        public int ProducerId { get; set; }
        
    }
}
