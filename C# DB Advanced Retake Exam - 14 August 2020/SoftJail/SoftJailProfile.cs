namespace SoftJail
{
    using AutoMapper;
    using SoftJail.Data.Models;
    using SoftJail.DataProcessor.ImportDto.Department;
    using SoftJail.DataProcessor.ImportDto.Prisoners;

    public class SoftJailProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public SoftJailProfile()
        {

            this.CreateMap<ImportCellDto, Cell>();
            this.CreateMap<ImportMailDto, Mail>();
            this.CreateMap<ImportPrisonerMailDto, Prisoner>()
                .ForMember(member => member.Mails, opt => opt.Ignore());
            this.CreateMap<ImportPrisonerMailDto, ImportPrisonerMailDto>();
            

        }
    }
}
