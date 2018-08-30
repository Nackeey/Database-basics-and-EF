namespace PetClinic.App
{
    using AutoMapper;
    using PetClinic.Dtos.ImportDto;
    using PetClinic.Models;
    using System;

    public class PetClinicProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public PetClinicProfile()
        {
            CreateMap<AnimalAidDto, AnimalAid>();
            CreateMap<AnimalDto, Animal>();
            CreateMap<PassportDto, Passport>();
            CreateMap<VetDto, Vet>();
        }
    }
}
