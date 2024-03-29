﻿namespace Artillery
{
    using Artillery.Data.Models;
    using Artillery.DataProcessor.ImportDto;
    using AutoMapper;

    public class ArtilleryProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public ArtilleryProfile()
        {

            this.CreateMap<ImportCountryDto, Country>();
            this.CreateMap<ImportManufacturerDto, Manufacturer>();
            this.CreateMap<ImportShellDto, Shell>();
            this.CreateMap<ImportGunsDto, Gun>();
                   //.ForMember(dest => dest.CountriesGuns, opt => opt.MapFrom(d => d.Countries));
                //.ForSourceMember(d => d.Countries, opt => opt.DoNotValidate());

        }
    }
}