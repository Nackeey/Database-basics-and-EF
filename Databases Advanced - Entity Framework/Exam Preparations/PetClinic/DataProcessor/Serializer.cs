namespace PetClinic.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.Dtos.ExportDto;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Animals.Where(x => x.Passport.OwnerPhoneNumber == phoneNumber)
                                         .Select(x => new AnimalDto
                                         {
                                             OwnerName = x.Passport.OwnerName,
                                             AnimalName = x.Name,
                                             Age = x.Age,
                                             SerialNumber = x.PassportSerialNumber,
                                             RegisteredOn = x.Passport.RegistrationDate
                                         })
                                         .OrderBy(x => x.Age)
                                         .ThenBy(x => x.SerialNumber)
                                         .ToArray();

            var jsonString = JsonConvert.SerializeObject(animals, Newtonsoft.Json.Formatting.Indented);

            return jsonString;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedures = context.Procedures
                                    .OrderBy(x => x.DateTime)
                                    .Select(x => new ExportProcedureDto
                                    {
                                        Passport = x.Animal.Passport.SerialNumber,
                                        OwnerNumber = x.Animal.Passport.OwnerPhoneNumber,
                                        DateTime = x.DateTime.ToString("dd-MM-yyyy"),
                                        AnimalAids = x.ProcedureAnimalAids.Select(d => new ExportAnimalAidDto
                                        {
                                            Name = d.AnimalAid.Name,
                                            Price = d.AnimalAid.Price
                                        })
                                        .ToArray(),
                                        TotalPrice = x.ProcedureAnimalAids.Sum(e => e.AnimalAid.Price)
                                    })
                                    .OrderBy(x => x.Passport)
                                    .ToArray();

            var sb = new StringBuilder();

            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(ExportProcedureDto[]), new XmlRootAttribute("Procedures"));

            serializer.Serialize(new StringWriter(sb), procedures, xmlNamespaces);

            return sb.ToString().TrimEnd();
        }
    }
}
