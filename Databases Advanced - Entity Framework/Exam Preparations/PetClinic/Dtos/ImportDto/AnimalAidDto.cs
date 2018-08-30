using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.Dtos.ImportDto
{
    public class AnimalAidDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "1000000000.000")]
        public decimal Price { get; set; }
    }
}
