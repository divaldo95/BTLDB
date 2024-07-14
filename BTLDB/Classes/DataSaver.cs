using System;
using BTLDB.Data;
using BTLDB.Models;
using Microsoft.EntityFrameworkCore;

namespace BTLDB.Classes
{
    public class DataSaver
    {
        private readonly ApplicationDbContext _context;

        public DataSaver(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveData(IEnumerable<SiPMArray> sipmArrays)
        {
            foreach (var array in sipmArrays)
            {
                var existingArray = _context.SiPMArrays
                    .Include(a => a.Channels)  // Assuming you have a navigation property for Channels
                    .FirstOrDefault(a => a.SN == array.SN);

                if (existingArray == null)
                {
                    // Add new array with channels
                    _context.SiPMArrays.Add(array);
                }
                else
                {
                    // Update existing array channels if necessary
                    foreach (var channel in array.Channels)
                    {
                        var existingChannel = existingArray.Channels
                            .FirstOrDefault(c => c.ChNo == channel.ChNo);

                        if (existingChannel == null)
                        {
                            existingArray.Channels.Add(channel);
                        }
                        else
                        {
                            // Update channel data if necessary
                            existingChannel.Vop = channel.Vop;
                            existingChannel.Id1 = channel.Id1;
                            existingChannel.Id2 = channel.Id2;
                            existingChannel.Is = channel.Is;
                            existingChannel.RqN = channel.RqN;
                        }
                    }
                    existingArray.TECResistance = array.TECResistance;
                    existingArray.MeanResistance = array.MeanResistance;
                    existingArray.StdDevResistance = array.StdDevResistance;
                    existingArray.RTD = array.RTD;
                }
            }
            _context.SaveChanges();
        }
    }
}

