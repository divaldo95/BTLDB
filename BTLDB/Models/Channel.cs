using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTLDB.Models
{
    public class Channel
    {
        [Key]
        public int Id { get; set; }
        public int ChNo { get; set; }
        public double Vop { get; set; }
        public double Id1 { get; set; }
        public double Id2 { get; set; }
        public double Is { get; set; }
        public double RqN { get; set; }

        [ForeignKey("SiPMArray")]
        public int SiPMArrayId { get; set; }
        public SiPMArray SiPMArray { get; set; }
    }
}

