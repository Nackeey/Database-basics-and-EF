namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.Dtos.ImportDto;
    using PetClinic.Models;

    public class Deserializer
    {
        private const string error_message = "Error: Invalid data.";

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var deserializedJson = JsonConvert.DeserializeObject<AnimalAidDto[]>(jsonString);

            var sb = new StringBuilder();
            var animalAids = new List<AnimalAid>();

            foreach (var animalAidDto in deserializedJson)
            {
                var animalAidExists = animalAids.Any(x => x.Name == animalAidDto.Name);

                if (!IsValid(animalAidDto) || animalAidExists)
                {
                    sb.AppendLine(error_message);
                    continue; 
                }

                var animalAid = Mapper.Map<AnimalAid>(animalAidDto);

                animalAids.Add(animalAid);
                sb.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

                context.AnimalAids.AddRange(animalAids);
                context.SaveChanges();

                return sb.ToString().TrimEnd();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var deserializedJson = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);
            var sb = new StringBuilder();

            var animals = new List<Animal>();

            foreach (var animalDto in deserializedJson)
            {
                var passportExists = animals.Any(x => x.Passport.SerialNumber == animalDto.Passport.SerialNumber);

                if (!IsValid(animalDto) || !IsValid(animalDto.Passport) || passportExists)
                {
                    sb.AppendLine(error_message);
                    continue;
                }

                var animal = Mapper.Map<Animal>(animalDto);

                animals.Add(animal);

                sb.AppendLine($"Record {animal.Name} Passport №: {animal.Passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(animals);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var deserializedVets = (VetDto[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
            var vets = new List<Vet>();

            foreach (var vetDto in deserializedVets)
            {
                var phoneNumberExists = vets.Any(x => x.PhoneNumber == vetDto.PhoneNumber);

                if (!IsValid(vetDto) || phoneNumberExists)
                {
                    sb.AppendLine(error_message);
                    continue;
                }

                var vet = Mapper.Map<Vet>(vetDto);
                vets.Add(vet);

                sb.AppendLine($"Record {vet.Name} successfully imported.");
            }

            context.Vets.AddRange(vets);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var deserializedXml = (ProcedureDto[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
            var procedures = new List<Procedure>();

            foreach (var procedureDto in deserializedXml)
            {
                var vet = context.Vets.SingleOrDefault(x => x.Name == procedureDto.Vet);
                var animal = context.Animals.SingleOrDefault(x => x.PassportSerialNumber == procedureDto.Animal);
                var validProcedureAnimalAids = new List<ProcedureAnimalAid>();
                var allAidsExists = true;

                foreach (var procedureAnimalAidDto in procedureDto.AnimalAids)
                {
                    var animalAid = context.AnimalAids.SingleOrDefault(x => x.Name == procedureAnimalAidDto.Name);

                    if (animalAid == null || validProcedureAnimalAids.Any(x => x.AnimalAid.Name == procedureAnimalAidDto.Name))
                    {
                        allAidsExists = false;
                        break;
                    }

                    var procedureAnimalAid = new ProcedureAnimalAid()
                    {
                        AnimalAid = animalAid
                    };

                    validProcedureAnimalAids.Add(procedureAnimalAid);
                }

                if (!IsValid(procedureDto) 
                    || vet == null 
                    || animal == null 
                    || !procedureDto.AnimalAids.All(IsValid) 
                    || !allAidsExists)
                {
                    sb.AppendLine(error_message);
                    continue;
                }

                var procedure = new Procedure
                {
                    Animal = animal,
                    Vet = vet,
                    DateTime = DateTime.ParseExact(procedureDto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                    ProcedureAnimalAids = validProcedureAnimalAids
                };

                procedures.Add(procedure);
                sb.AppendLine($"Record successfully imported.");
            }

            context.Procedures.AddRange(procedures);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, validationContext, results, true);

            return isValid;
        }
    }
}
