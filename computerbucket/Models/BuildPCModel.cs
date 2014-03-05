using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace computerbucket.Models
{
    public class BuildPCModel
    {
        [Required]
        public string Motherboard { get; set; }
        [Required]
        public string Processor { get; set; }
        [Required]
        public string GraphicCard { get; set; }
        [Required]
        public string Ram { get; set; }
        [Required]
        public string HardDrive { get; set; }
        [Required]
        public string Ssd { get; set; }
        [Required]
        public string PowerSupply { get; set; }
        [Required]
        public string CpuCooling { get; set; }
        [Required]
        public string ThermalPaste { get; set; }
        [Required]
        public string InternalDrive { get; set; }
        [Required]
        public string Os { get; set; }
        [Required]
        public string ComputerCase { get; set; }

    }
}