﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class GenerateTokenVm
    {
        [Required]
        [MaxLength(64)]
        public string Username { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
