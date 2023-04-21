using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_TourDetail : EntityBase
    {
        public string TourCode { get; set; }
        public string IDNo { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public int TouristNumberAll { get; set; }
        public int TouristNumberLeft { get; set; }
        public string TourGuide1 { get; set; }
        public string TourGuide2 { get; set; }
        public string GatherDate { get; set; }
        public string GatherTime { get; set; }
        public string GatherAddress { get; set; }
        public string GoFlightNo { get; set; }
        public string ReturnFlightNo { get; set; }
        public string CreateDTime { get; set; }
        public string CreateBy { get; set; }
        public string LogLUDTime { get; set; }
        public string LogLUBy { get; set; }
        ////
        public string mt_TourName { get; set; }
        public string mt_TourType { get; set; }
        public string mtt_TourTypeName { get; set; }
        public string mt_TourDesc { get; set; }
        public string mt_TourThemePath { get; set; }
        public string mt_TourDuration { get; set; }
        public int mt_TourDayDuration { get; set; }
        public int mt_TourNightDuration { get; set; }
        public int mt_TourTouristNumber { get; set; }
        public string mt_TourTransport { get; set; }
        public string mt_TourListDest { get; set; }
        public string mt_TourFood { get; set; }
        public string mt_TourHotel { get; set; }
        public string mt_TourIdealTime { get; set; }
        public string mt_TourIdealPeople { get; set; }
        public string mt_TourPreferential { get; set; }
        public string mt_TourStartPoint { get; set; }
        public double mt_TourPrice { get; set; }
        public string mt_TourImage1Path { get; set; }
        public string mt_TourImage2Path { get; set; }
        public string mt_TourImage3Path { get; set; }
        public string mt_TourImage4Path { get; set; }

        ////
        //public List<DA_Mst_TourDetailDate> Mst_TourDetailDate { get; set; }
        //public List<DA_Mst_TourSchedule> Mst_TourSchedule { get; set; }
        //public List<DA_Mst_TourScheduleDetail> Mst_TourScheduleDetail { get; set; }
        //public List<DA_Mst_TourDestImages> Mst_TourDestImages { get; set; }

    }
}
