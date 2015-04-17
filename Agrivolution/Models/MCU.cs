using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Agrivolution.Models
{
    public class MCU
    {
        [Required, ScaffoldColumn(false)]
        public int MCUID { get; set; }

        [Required, Display(Name = "Name")]
        public string Room { get; set; }

        [Required]
        public String Facility { get; set; }

        [Required, Display(Name = "Name")]
        public string UserID { get; set; }

        [Required, Display(Name = "Product Description"),
    DataType(DataType.MultilineText)]
        public string Type { get; set; }

        public string Status { get; set; }

        [ Display(Name = "Name")]
        public string Group { get; set; }

        [ Display(Name = "Name")]
        public List<int> Schedule { get; set; }

        [Display(Name = "Price")]
        public List<String> MCUData { get; set; }

        public List<String> MCUPhotos { get; set; }
    }
}