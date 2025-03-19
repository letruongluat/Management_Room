using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Room
{
    public class MRoom
    {
        public int RoomNumber { get; set; }
        public bool IsOccupied { get; set; }
        public bool Clean { get; set; } = false;
        public double Price { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public DateTime? OrdDate { get; set; }
        public Guest GuestInfo { get; set; }

        public void AssignGuest(Guest guest, DateTime checkIn, DateTime checkOut)
        {
            GuestInfo = guest;
            CheckInDate = checkIn;
            CheckOutDate = checkOut;
            IsOccupied = true;
        }

        public void DisplayRoomInfo()
        {
            string status = IsOccupied ? "Da su dung" : "Trong";
            Console.WriteLine($"Phong: {RoomNumber} | Trang thai: {status} | Gia: {Price}");

            if (GuestInfo != null)
            {
                GuestInfo.DisplayInfo();
                Console.WriteLine($"Ngay check-in: {CheckInDate} | Ngay check-out: {CheckOutDate}");
            }
        }
    }
}
