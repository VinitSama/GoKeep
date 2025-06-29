﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoKeep.Model
{
    public class UpdateLabelRequestModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
    }
}
