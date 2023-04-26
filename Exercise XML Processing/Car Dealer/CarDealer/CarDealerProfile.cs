using AutoMapper;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using System.Globalization;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<ImportSuplierDto, Supplier>();
            CreateMap<ImportPartDto, Part>()
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(s => s.SupplierId.Value));

            CreateMap<ImportCarDto, Car>()
                .ForSourceMember(s => s.Parts, opt => opt.DoNotValidate());

            CreateMap<ImportCustomerDto, Customer>()
                .ForMember(dest => dest.BirthDate, opt => 
                    opt.MapFrom(s => DateTime.Parse(s.BirthDate,CultureInfo.InvariantCulture)));

            CreateMap<Car, ExportCarDto>();
        }
    }
}
