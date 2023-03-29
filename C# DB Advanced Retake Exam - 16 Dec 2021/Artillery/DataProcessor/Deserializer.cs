namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;
    using Artillery.Utilities;
    using AutoMapper;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            IMapper mapper = CreateMapper();
            StringBuilder sb = new StringBuilder();

            string rootElement = "Countries";
            ImportCountryDto[] countryDtos = XmlSerialization.DeserializeXml<ImportCountryDto>(xmlString, rootElement);
            ICollection<Country> countries = new HashSet<Country>();

            foreach(var country in countryDtos)
            {
                if (!IsValid(country))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Country validCountry=mapper.Map<Country>(country);
                countries.Add(validCountry);
                sb.AppendLine(string.Format(SuccessfulImportCountry, validCountry.CountryName, validCountry.ArmySize));
            }

            context.Countries.AddRange(countries);
            context.SaveChanges();

            return sb.ToString().Trim();
        }
        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            IMapper mapper = CreateMapper();
            StringBuilder sb = new StringBuilder();
            string rootElement = "Manufacturers";

            ImportManufacturerDto[] manufacturerDtos = XmlSerialization.DeserializeXml<ImportManufacturerDto>(xmlString,rootElement);
            Console.WriteLine(manufacturerDtos.Length);
            ICollection<Manufacturer> manufacturers = new HashSet<Manufacturer>();

            foreach(var manufacturer in manufacturerDtos)
            {
                if (!IsValid(manufacturer))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                string[] arguments = manufacturer.Founded.Split(", ");
                string date = arguments[0];

                DateTime dateTime;
                bool validDate
                    = DateTime.TryParseExact(date, "dd/MMM/yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None,out dateTime);

                if (!validDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Manufacturer validManufacturer = mapper.Map<Manufacturer>(manufacturer);
                manufacturers.Add(validManufacturer);
                sb.AppendLine(string.Format(SuccessfulImportManufacturer, validManufacturer.ManufacturerName, validManufacturer.Founded));
            }

            return sb.ToString().Trim();
        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            IMapper mapper = CreateMapper();
            StringBuilder sb = new StringBuilder();
            string rootElement = "Shells";

            ImportShellDto[] shellDtos = XmlSerialization.DeserializeXml<ImportShellDto>(xmlString, rootElement);
            ICollection<Shell> shells = new HashSet<Shell>();

            foreach(var shell in shellDtos)
            {
                if (!IsValid(shell))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Shell validShell=mapper.Map<Shell>(shell);
                shells.Add(validShell);
                sb.AppendLine(string.Format(SuccessfulImportShell, validShell.Caliber, validShell.ShellWeight));
            }

            context.AddRange(shells);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            IMapper mapper = CreateMapper();
            StringBuilder sb = new StringBuilder();

            ImportGunsDto[] gunsDtos = JsonConvert.DeserializeObject<ImportGunsDto[]>(jsonString);

            ICollection<Gun> guns = new HashSet<Gun>();

            foreach(var gun in gunsDtos)
            {

                if (!IsValid(gun))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if(!Enum.IsDefined(typeof(GunType), gun.GunType))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Gun validGun=mapper.Map<Gun>(gun);
                foreach(var countryId in gun.Countries)
                {
                    validGun.CountriesGuns.Add(new CountryGun
                    {
                        CountryId = countryId.Id,
                        Gun = validGun
                    });
                }
                guns.Add(validGun);
                sb.AppendLine(string.Format(SuccessfulImportGun, validGun.GunType, validGun.GunWeight, validGun.BarrelLength));

            }

            context.AddRange(guns);
            context.SaveChanges();
            return sb.ToString().Trim();
        }


        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }

        private static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArtilleryProfile>();
            });
            IMapper mapper = new Mapper(configuration);
            return mapper;
        }
    }
}