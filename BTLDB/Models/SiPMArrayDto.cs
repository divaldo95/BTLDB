using System;
using static BTLDB.Models.Channeldto;

namespace BTLDB.Models
{
    public class SiPMArrayDto
    {
        public string TrayNo { get; set; }
        public int PositionNo { get; set; }
        public string SN { get; set; }
        public double TECResistance { get; set; }
        public double MeanResistance { get; set; }
        public double StdDevResistance { get; set; }
        public double RTD { get; set; }
        public List<ChannelDto> Channels { get; set; }
    }
}

