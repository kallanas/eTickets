using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage ="The Profile Picture is required")]
        public string ProfilePictureURL { get; set; }
        [Required(ErrorMessage = "The Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The full name must be between 3 and 50 characters.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "The Bio is required")]
        public string  Bio { get; set; }

        //RelationShips
        public List<Movie> Movies { get; set; }
    }
}
