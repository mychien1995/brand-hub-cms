using System;
using System.Collections.Generic;
using System.Text;

namespace BrandHub.Models.Adresses
{
    public class AddressModel
    {
        public int ID { get; set; }
        public string AddressLine { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int? DistrictId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
