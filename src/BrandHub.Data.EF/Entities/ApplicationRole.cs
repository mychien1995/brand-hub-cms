﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BrandHub.Data.EF.Entities
{
    public class ApplicationRole
    {
        [Key]
        public int ID { get; set; }
        public string RoleName { get; set; }
    }
}
