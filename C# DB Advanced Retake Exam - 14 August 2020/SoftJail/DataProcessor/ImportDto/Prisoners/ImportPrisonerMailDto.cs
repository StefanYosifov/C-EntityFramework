namespace SoftJail.DataProcessor.ImportDto.Prisoners
{
    using Newtonsoft.Json;
    using SoftJail.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ImportPrisonerMailDto
    {
        [Required]
        [MinLength(ValidationConstants.PrisonerFullNameMinLength)]
        [MaxLength(ValidationConstants.PrisonerFullNameMaxLength)]
        [JsonProperty(nameof(FullName))]
        public string FullName { get; set; }

        [RegularExpression(ValidationConstants.PrisonerNickRegex)]
        [JsonProperty(nameof(Nickname))]
        public string Nickname { get; set; }

        [Required]
        [Range(ValidationConstants.PrisonerAgeMin,ValidationConstants.PrisonerAgeMax)]
        [JsonProperty(nameof(Age))]
        public int Age { get; set; }


        [JsonProperty(nameof(IncarcerationDate))]
        public DateTime IncarcerationDate { get; set; }

        [JsonProperty(nameof(ReleaseDate))]
        public DateTime ReleaseDate { get; set; }

        [Range(typeof(decimal),"0","9999999")]
        [JsonProperty(nameof(Bail))]

        public decimal? Bail { get; set; }

        [JsonProperty(nameof(CellId))]

        public int CellId { get; set; }

        [JsonProperty(nameof(Mails))]
        public ImportPrisonerMailDto[] Mails{ get; set; }

    }
}
