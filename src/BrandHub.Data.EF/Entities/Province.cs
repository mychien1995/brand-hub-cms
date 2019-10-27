using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrandHub.Data.EF.Entities
{
    public class Province
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public virtual List<District> Districts { get; set; }
        public virtual List<Address> Addresses { get; set; }

        public virtual List<Organization> Organizations { get; set; }
    }
}
