using System.Xml.Serialization;

namespace VaporStore.Data.Models.ExportDtos
{
    [XmlType("Purchase")]
    public class PurchaseDto
    {
        public string Card { get; set; }

        public string Cvc { get; set; }

        public string Date { get; set; }

        public PurchaseGameDto Game { get; set; }
    }
}