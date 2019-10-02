using System;
using System.Collections.Generic;
using System.Linq;

namespace DayVsNight.Models
{
    public class Room
    {
        public string Name { get; set; }
        public List<Device> Devices { get; set; } = new List<Device>();

        public string DevicesActive => $"{Devices.Where(i => i.IsActive).Count()} active devices";
    }
}