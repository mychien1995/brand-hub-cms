using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrandHub.Data.EF.Entities
{
    public class ApplicationUserToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Value { get; set; }
        public int UserId { get; set; }

        public DateTime StartedTime { get; set; }
        public DateTime ExpiredTime { get; set; }
    }
}
