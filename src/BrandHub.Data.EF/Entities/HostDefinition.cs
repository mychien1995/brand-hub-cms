using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrandHub.Data.EF.Entities
{
    public class HostDefinition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int OrganizationId { get; set; }
        public string HostName { get; set; }

        [ForeignKey("OrganizationId")]
        public virtual Organization Organization { get; set; }

    }
}
