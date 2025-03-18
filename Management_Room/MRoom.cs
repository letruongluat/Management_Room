using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Room
{
    class MRoom
    {
        public int RoomNumber { get; set; }
        public bool IsOccupied { get; set; }
        public bool clean { get; set; } = false;
        public Guest GuestInFo { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public DateTime? OrdDate { get; set; }
        public double price { get; set; } = 5000;
        
        public void Check()
        {
            Console.WriteLine($"Phong {RoomNumber} - {(IsOccupied ? "Đa co nguoi" : "Trong")}");
           
        }
        

    }
}
