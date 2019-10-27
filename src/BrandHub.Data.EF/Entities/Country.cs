using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BrandHub.Data.EF.Entities
{
    public class Country
    {
        [Key]
        public int ID { get; set; }

        public string CountryCode { get; set; }
        public string Name { get; set; }

        public virtual List<Province> Provinces { get; set; }

        public virtual List<Address> Addresses { get; set; }
    }
}
