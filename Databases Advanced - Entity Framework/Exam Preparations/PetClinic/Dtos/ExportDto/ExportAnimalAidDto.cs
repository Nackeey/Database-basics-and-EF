using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.Dtos.ExportDto
{
    [XmlType("AnimalAid")]
    public class ExportAnimalAidDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
