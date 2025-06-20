using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BOL.DTOs
{
    public class HotelReportResponse
    {
        public int globalTotalRooms { get; set; }
        public int globalAvailableRooms { get; set; }
        public float globalAvailableRoomsPercent { get; set; }
        public List<HotelReport> hotelList { get; set; } = new List<HotelReport>();
        public class HotelReport
        {
            public string name { get; set; }
            public string address_name { get; set; }
            public int totalRooms { get; set; }
            public int availableRooms { get; set; }
            public float availableRoomsPercent { get; set; }
            public HotelReport()
            {
                
            }
        }
    }

}
