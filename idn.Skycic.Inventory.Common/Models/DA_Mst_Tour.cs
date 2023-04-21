using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Attributes;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_Tour : EntityBase
    {
        public string TourCode { get; set; }
        public string TourName { get; set; }
        public string TourType { get; set; }
        public string TourDesc { get; set; }
        public string TourThemePath { get; set; }
        public string TourImage1Path { get; set; }
        public string TourImage2Path { get; set; }
        public string TourImage3Path { get; set; }
        public string TourImage4Path { get; set; }
        public string TourDuration { get; set; }
        public int TourDayDuration { get; set; }
        public int TourNightDuration { get; set; }
        public int TourTouristNumber { get; set; }
        public string TourTransport { get; set; }
        public string TourListDest { get; set; }
        public string TourFood { get; set; }
        public string TourHotel { get; set; }
        public string TourIdealTime { get; set; }
        public string TourIdealPeople { get; set; }
        public string TourPreferential { get; set; }
        public string TourStartPoint { get; set; }
        public double TourPrice { get; set; }
        public string FlagActive { get; set; }
        public string CreateDTime { get; set; }
        public string CreateBy { get; set; }
        public string LogLUDTime { get; set; }
        public string LogLUBy { get; set; }
    }
}
