﻿using System.Xml.Serialization;

namespace VaporStore.Data.Models.ExportDtos
{
    [XmlType("Game")]
    public class PurchaseGameDto
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }
    }
}