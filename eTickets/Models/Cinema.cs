using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Cinema :IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Logo is required.")]
        public string Logo { get; set; }
        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage ="Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Cinema name must be between 3 and 50 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
