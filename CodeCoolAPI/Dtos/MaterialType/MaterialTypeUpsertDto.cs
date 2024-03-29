﻿using System.ComponentModel.DataAnnotations;

namespace CodeCoolAPI.Dtos
{
    public class MaterialTypeUpsertDto
    {
        [Required] [MaxLength(30)] 
        public string Name { get; set; }

        [MaxLength(120)] 
        public string Definition { get; set; }
    }
}