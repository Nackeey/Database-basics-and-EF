using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.Data.Models.ExportDtos
{
    [XmlType("User")]
    public class UserDto
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        public PurchaseDto[] Purchases { get; set; }

        public decimal TotalSpent { get; set; }
    }
}
