namespace SoftJail.DataProcessor.ImportDto.Prisoners
{
    using Newtonsoft.Json;
    using SoftJail.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ImportMailDto
    {
        [Required]
        [JsonProperty(nameof(Description))]

        public string Description { get; set; }

        [Required]
        [JsonProperty(nameof(Sender))]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(ValidationConstants.MailValidateAdressRegex)]
        [JsonProperty(nameof(Address))]
        public string Address { get; set; }
    }
}
