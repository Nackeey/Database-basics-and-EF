using PetClinic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.Dtos.ImportDto
{
    [XmlType("Procedure")]
    public class ProcedureDto
    {
        [Required]
        public string Vet { get; set; }

        [Required]
        public string Animal { get; set; }

        [Required]
        public string DateTime { get; set; }

        public AnimalAidDtoName[] AnimalAids { get; set; }
    }
}
