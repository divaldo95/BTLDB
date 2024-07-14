using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;

namespace BTLDB.Models
{
    public class SiPMArray
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SN { get; set; }
        public string TrayNo { get; set; }
        public int PositionNo { get; set; }
        public double TECResistance { get; set; }
        public double MeanResistance { get; set; }
        public double StdDevResistance { get; set; }
        public double RTD { get; set; }
        public ICollection<Channel> Channels { get; set; }
    }
}

