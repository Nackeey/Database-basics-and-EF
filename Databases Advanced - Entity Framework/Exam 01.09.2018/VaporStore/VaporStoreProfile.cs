namespace VaporStore
{
	using AutoMapper;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.ImportDtos;

    public class VaporStoreProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public VaporStoreProfile()
		{
            CreateMap<GameDto, Game>();
            CreateMap<CardDto, Card>();
            CreateMap<UserDto, User>();
		}
	}
}