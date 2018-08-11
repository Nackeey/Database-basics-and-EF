using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.App.Dto
{
    [XmlType("sold-product")]
    public class SoldProductDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public ProductDto[] ProductDto { get; set; }
    }
}
