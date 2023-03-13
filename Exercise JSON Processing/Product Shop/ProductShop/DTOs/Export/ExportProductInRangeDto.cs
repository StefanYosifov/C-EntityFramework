namespace ProductShop.DTOs.Export
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExportProductInRangeDto
    {
        [JsonProperty("name")]
        public string ProductName { get; set; }

        [JsonProperty("price")]

        public decimal ProductPrice { get; set; }

        [JsonProperty("seller")]

        public string SellerName { get; set; }
    }

}
