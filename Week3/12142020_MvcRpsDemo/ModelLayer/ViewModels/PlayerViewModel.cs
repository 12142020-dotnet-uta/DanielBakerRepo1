using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ModelLayer.ViewModels
{
    public class PlayerViewModel
    {
        public PlayerViewModel(string fname = "null", string lname = "null")
        {
            this.Fname = fname;
            this.Lname = lname;
        }

        public Guid PlayerId { get; set; } = Guid.NewGuid();

        [StringLength(20, MinimumLength = 2, ErrorMessage = "The name must be from 3 to 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "The name must be from 3 to 20 characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Required]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Number of Wins")]
        public int numWins { get; set; }

        [Range(0, int.MaxValue)]
        [Display(Name = "Number of Losses")]
        public int numLosses { get; set; }
        public string JpegStringImage { get; set; }

        public IFormFile IformFileImage { get; set; }
    }
}
