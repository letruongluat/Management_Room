using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Room
{
    class Room
    {
        public static List<MRoom> rooms = new List<MRoom>();
       
        string PassAdmin = "123";
        string UserName = "admin";
        private readonly string filePath = "C:\\Users\\LUAT\\Downloads\\rooms.txt";
        
        public Room()
        {
            rooms = LoadRoomsFromFile();
            
        }
        public void LoginGuest()
        {
            while (true)
            {
                Console.WriteLine("\n====================| Khach Hang |====================");
                Console.WriteLine("|               1. Search.                            |");
                Console.WriteLine("|               2. Order Room.                        |");
                Console.WriteLine("|               3. thoat.                             |");
                Console.WriteLine("|               vui long chon so!!                    |");
                Console.WriteLine("=======================================================");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    
                    case "1": searchGuest(); break;
                    case "2": orderRoom(); break;
                    case "3": return;
                }
            }
        }
        public void LoginADM()
        {
            Console.WriteLine("\n=====================================");
            Console.Write("Ten Tai Khoan:");
            string user = Console.ReadLine();
            Console.Write("Mat Khau:");
            string pass = Console.ReadLine();
            Console.WriteLine("=====================================");
            if (pass == PassAdmin && user == UserName)
            {
                Console.WriteLine("Dang Nhap Thanh Cong.");
                while (true)
                {


                    Console.WriteLine("\n===================| Quan Tri Vien |==================");
                    Console.WriteLine("|               1. Quan Ly Phong.                     |");
                    Console.WriteLine("|               2. Quan Ly Khach Hang.                |");
                    Console.WriteLine("|               3. Tao va xuat hoa don.               |");
                    Console.WriteLine("|               4. Search.                            |");
                    Console.WriteLine("|               5. thoat.                             |");
                    Console.WriteLine("|               vui long chon so!!                    |");
                    Console.WriteLine("=======================================================");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1": ManagerRoom(); break;
                        case "2": ManagerGuest(); break;
                        case "3": PrintBill(); break;
                        case "4": search(); break;
                        
                        case "5": return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Dang Nhap That Bai.");
            }
        }
      
        public void AddRoom()
        {
            Console.WriteLine("Nhap so phong muon them:");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                if (rooms.Any(r => r.RoomNumber == roomNumber))
                {
                    Console.WriteLine("Phong da ton tai.");
                }
                else
                {
                    rooms.Add(new MRoom { RoomNumber = roomNumber, IsOccupied = false });
                    SaveRoomsToFile();
                    Console.WriteLine("Them phong thanh cong.");
                    PrintAllRooms();
                }
            }
            else
            {
                Console.WriteLine("So phong khong hop le.");
            }
        }




        private void SaveRoomsToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var room in rooms)
                {
                    string guestInfo = room.GuestInFo != null
                        ? $"{room.GuestInFo.Name}|{room.GuestInFo.Gender}|{room.GuestInFo.Age}"
                        : "||";

                    writer.WriteLine($"{room.RoomNumber}|{room.IsOccupied}|{room.CheckInDate}|{room.CheckOutDate}|{guestInfo}");
                }
            }
        }


        private List<MRoom> LoadRoomsFromFile()
        {
            var loadedRooms = new List<MRoom>();

            if (!File.Exists(filePath))
                return loadedRooms;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('|');
                if (parts.Length >= 5)
                {
                    var room = new MRoom
                    {
                        RoomNumber = int.Parse(parts[0]),
                        IsOccupied = bool.Parse(parts[1]),
                        CheckInDate = DateTime.TryParse(parts[2], out DateTime checkin) ? checkin : DateTime.MinValue,
                        CheckOutDate = DateTime.TryParse(parts[3], out DateTime checkout) ? checkout : DateTime.MinValue,
                        GuestInFo = string.IsNullOrWhiteSpace(parts[4]) ? null : new Guest
                        {
                            Name = parts[4],
                            Gender = parts[5],
                            Age = int.Parse(parts[6])
                        }
                    };

                    loadedRooms.Add(room);
                }
            }

            return loadedRooms;
        }

        public void RemoveRoom()
        {
            Console.WriteLine("Nhap so phong muon xoa:");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room != null)
                {
                    rooms.Remove(room);
                    Console.WriteLine("Xoa phong thanh cong.");
                    PrintAllRooms();
                }
                else
                {
                    Console.WriteLine("Phong khong ton tai.");
                }
            }
            else
            {
                Console.WriteLine("So phong khong hop le.");
            }
        }

        public void UpdateRoom()
        {
            Console.WriteLine("Nhap so phong muon cap nhat:");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room != null)
                {
                    Console.WriteLine("Nhap gia tien moi:");
                    if (double.TryParse(Console.ReadLine(), out double newPrice))
                    {
                        room.price = newPrice;
                        Console.WriteLine("Cap nhat gia phong thanh cong.");
                    }
                    else
                    {
                        Console.WriteLine("Gia tien khong hop le.");
                    }
                }
                else
                {
                    Console.WriteLine("Phong khong ton tai.");
                }
            }
            else
            {
                Console.WriteLine("So phong khong hop le.");
            }
        }
        public void PrintAllRooms()
        {
            if (rooms.Count == 0)
            {
                Console.WriteLine("Chua co phong nao. Hay them phong truoc.");
                return;
            }
            Console.WriteLine("\nDanh sach cac phong:");
            Console.WriteLine("\nSTT | Ten phong | Trang thai | Gia tien | Trong/Da su dung");
            int stt = 1;
            foreach (var room in rooms)
            {
                string status = room.IsOccupied ? "Da su dung" : "Trong";
                Console.WriteLine($"{stt}   | Phong {room.RoomNumber}   | {(room.clean ? "Can don dep" : "San sang")}   | {room.price}     | {status}");
                stt++;
            }
        }
      
        public void AddGuest()
        {
            Console.WriteLine("\nDanh sach cac phong:");
            Console.WriteLine("\nSTT | Ten phong | Trang thai | Gia tien | Trong/Da su dung");
            int stt = 1;
            foreach (var room in rooms)
            {
                string status = room.IsOccupied ? "Da su dung" : "Trong";
                Console.WriteLine($"{stt}   | Phong {room.RoomNumber}   | {(room.clean ? "Can don dep" : "San sang")}   | {room.price}     | {status}");
                stt++;
            }

            Console.WriteLine("Nhap so phong muon them khach:");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

                if (room != null)
                {
                    if (room.IsOccupied)
                    {
                        Console.WriteLine("Phong da co nguoi su dung.");
                    }
                    else
                    {
                        Console.WriteLine("Nhap ten khach:");
                        string name = Console.ReadLine();

                        Console.WriteLine("Nhap gioi tinh:");
                        string gender = Console.ReadLine();

                        Console.WriteLine("Nhap ngay check in :");
                        DateTime checkIn = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Nhap ngay check out :");
                        DateTime checkout = DateTime.Parse(Console.ReadLine());

                        Console.WriteLine("Nhap tuoi khach:");
                        if (int.TryParse(Console.ReadLine(), out int age))
                        {
                            room.CheckInDate = checkIn;
                            room.CheckOutDate = checkout;
                            room.GuestInFo = new Guest { Name = name, Gender = gender, Age = age };
                            room.IsOccupied = true;

                            SaveRoomsToFile();
                            
                            Console.WriteLine("Them khach thanh cong va da cap nhat file.");
                        }
                        else
                        {
                            Console.WriteLine("Tuoi khong hop le.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Phong khong ton tai.");
                }
            }
            else
            {
                Console.WriteLine("So phong khong hop le.");
            }
        }
        public void RemoveGuest()
        {
            Console.WriteLine("Nhap so phong muon xoa khach:");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room != null && room.IsOccupied)
                {
                    
                    room.GuestInFo = null;
                    room.IsOccupied = false;
                    Console.WriteLine("Xoa khach thanh cong. Phong hien tai trong.");
                }
                else
                {
                    Console.WriteLine("Phong khong co khach hoac khong ton tai.");
                }
            }
            else
            {
                Console.WriteLine("So phong khong hop le.");
            }
        }
        public void UpdateGuest()
        {
            Console.WriteLine("Nhap so phong co khach can cap nhat:");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room != null && room.IsOccupied)
                {
                    Console.WriteLine("Nhap ten moi cua khach:");
                    room.GuestInFo.Name = Console.ReadLine();
                    Console.WriteLine("Nhap gioi tinh moi:");
                    Console.WriteLine("Nhap ngay check in:");
                    DateTime checkIn = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Nhap ngay check outn:");
                    DateTime checkout = DateTime.Parse(Console.ReadLine());
                    room.GuestInFo.Gender = Console.ReadLine();
                    Console.WriteLine("Nhap tuoi moi:");
                    if (int.TryParse(Console.ReadLine(), out int newAge))
                    {
                        room.CheckInDate = checkIn;
                        room.CheckOutDate = checkout;
                        room.GuestInFo.Age = newAge;
                        Console.WriteLine("Cap nhat thong tin khach thanh cong.");
                    }
                    else
                    {
                        Console.WriteLine("Tuoi khong hop le.");
                    }
                }
                else
                {
                    Console.WriteLine("Phong khong co khach hoac khong ton tai.");
                }
            }
            else
            {
                Console.WriteLine("So phong khong hop le.");
            }
        }
        public void ManagerGuest()
        {
            while (true)
            {
                Console.WriteLine("\n===========|Cac Chuc Nang Quan Ly Khach|============");
                Console.WriteLine("|            0. Xem danh sach khach.                |");
                Console.WriteLine("|            1. Them khach.                         |");
                Console.WriteLine("|            2. Xoa khach.                          |");
                Console.WriteLine("|            3. Cap nhat khach.                     |");   
                Console.WriteLine("|            4. Thoat.                              |");
                Console.WriteLine("====================================================");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0": PrintGuest(); break;
                    case "1": AddGuest(); break;
                    case "2": RemoveGuest(); break;
                    case "3": UpdateGuest(); break;
                    case "4": return;
                }
            }
        }
        public void ManagerRoom()
        {
            while (true)
            {
                    Console.WriteLine("\n===========|Cac Chuc Nang Quan Ly Phong|============");                      
                    Console.WriteLine("|            0. Xem danh sach phong.                |");
                    Console.WriteLine("|            1. Them phong.                         |");
                    Console.WriteLine("|            2. Xoa phong.                          |");
                    Console.WriteLine("|            3. Cap nhat phong.                     |");
                    Console.WriteLine("|            4. In tat ca cac phong trong.          |");
                    Console.WriteLine("|            5. In tat ca cac phong da su dung.     |");
                    Console.WriteLine("|            6. Chon phong can don dep.             |");
                    Console.WriteLine("|            7. Thoat.                              |");
                    Console.WriteLine("=====================================================");
            
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0": PrintAllRooms(); break;
                    case "1": AddRoom(); break;
                    case "2": RemoveRoom(); break;
                    case "3": UpdateRoom(); break;
                    case "4": PrintEmpty(); break;
                    case "5": PrintFull(); break;
                    case "6": ordClean(); break;
                    case "7": return;
                }
            }
        }

        public void PrintEmpty()
        {
            Console.WriteLine("\nDanh sach cac phong trong:");
            var empty = rooms.Where(r => !r.IsOccupied);
            foreach (var room in empty)
            {
                Console.WriteLine($"\nPhong {room.RoomNumber}");
            }
        }

        public void PrintFull()
        {
            Console.WriteLine("\nDanh sach phong da su dung:");
            var full = rooms.Where(r => r.IsOccupied);
            foreach (var room in full)
            {
                room.Check();
            }
        }

        public void CheckRoomInfo()
        {
            Console.Write("Nhap so phong kiem tra: ");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (rooms != null)
                {
                    room.Check();
                    if (room.IsOccupied)
                    {
                        Console.WriteLine($"Khach: {room.GuestInFo.Name}, Nhan phong: {room.CheckInDate}");
                    }
                }
                else
                {
                    Console.WriteLine("phong k ton tai.");
                }
            }
            else
            {
                Console.WriteLine("So phong k hop le.");
            }
        }

        public void PrintGuest()
        {
            Console.WriteLine("Danh sach tat ca cac khach hang.");
            var info = rooms.Where(r => r.IsOccupied && r.GuestInFo != null);
            foreach (var ds in info)
            {
                Console.WriteLine($"Khach: {ds.GuestInFo.Name}, Gioi tinh: {ds.GuestInFo.Gender}, Do tuoi: {ds.GuestInFo.Age}");
                Console.WriteLine($"Ngay Nhan Phong: {ds.CheckInDate}");
                Console.WriteLine($"Ngay Tra phong: {ds.CheckOutDate}");
                Console.WriteLine($"Thoi Gian luu tru la: {(ds.CheckOutDate.Value - ds.CheckInDate).TotalDays} ngay");
                double cost = (ds.CheckOutDate.Value - ds.CheckInDate).TotalHours;
                Console.WriteLine($"Tong Tien La: {cost * ds.price} Đ");

            }
        }
        public void updatePrice()
        {
            Console.WriteLine("Nhap so phong can sua gia.");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room != null)
                {
                    Console.WriteLine("Nhap gia moi.");
                    if (int.TryParse(Console.ReadLine(), out int priceRoom))
                    {
                        room.price = priceRoom;
                        Console.WriteLine("cap nhat gia thanh cong.");
                    }
                    else
                    {
                        Console.WriteLine("cap nhat gia that bai.");
                    }
                }
                else
                {
                    Console.WriteLine("so phong khong hop le.");
                }
            }
        }
        public void PrintBill()
        {
            Console.WriteLine("Nhap so phong.");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room != null && room.IsOccupied && room.CheckOutDate.HasValue)
                {
                    double cost = (room.CheckOutDate.Value - room.CheckInDate).TotalHours;
                    string Time = $"phong: {room.RoomNumber} \nNgay Nhan phong: {room.CheckInDate} \nNgay tra phong: {room.CheckOutDate}";
                    string bill = $"\n\nHoa don cho phong: {room.RoomNumber} \nKhach: {room.GuestInFo.Name} \nThoi gian luu tru: {(room.CheckOutDate.Value - room.CheckInDate).TotalDays} Ngay \nTong tien la: {cost * room.price} Đ";
                    File.WriteAllText($"C:\\Users\\LUAT\\Downloads\\bill_room{room.RoomNumber}.txt", bill);
                    SaveBill(bill);
                    SaveTime(Time);
                    Console.WriteLine("Hoa don da duoc xuat.");
                }
                else
                {
                    Console.WriteLine("xuat hoa don that bai.");
                }
            }
            else
            {
                Console.WriteLine("nhap so phong khong hop le.");
            }
        }
        private void Savestatus(int roomnumber, string status)
        {
            string savestatus = $"\n{DateTime.Now}: phong {roomnumber} - {status}\n";
            File.AppendAllText("C:\\Users\\LUAT\\Downloads\\room_status.log", savestatus);
        }
        private void SaveTime(string message)
        {
            File.AppendAllText("C:\\Users\\LUAT\\Downloads\\user.log", $"\n\n{DateTime.Now}, {message}");
        }
        private void SaveBill(string bill)
        {
            File.AppendAllText("C:\\Users\\LUAT\\Downloads\\bill.log", bill);
        }
        public void search()
        {
            Console.WriteLine("Nhap ten hoac so phong can tim.");
        
            string sear = Console.ReadLine();
            var room = rooms.Where(r => r.IsOccupied && (r.GuestInFo.Name.Contains(sear) || r.RoomNumber.ToString() == sear)).Select(r => $"Phong: {r.RoomNumber}, Khach: {r.GuestInFo.Name}").ToList();
           
                if (room.Any())
                {
                    Console.WriteLine("khach can tim:");
                    room.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("khong co phong va khach can tim.");
            }       
        }
        public void searchGuest()
        {
            Console.WriteLine("so phong can tim.");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {

                var room = rooms.FirstOrDefault(r =>r.RoomNumber == roomNumber );

                if (room != null)
                {
                    if (room.IsOccupied)
                    {
                        Console.WriteLine($"Phong {room.RoomNumber} da co nguoi su dung. xin vui long chon phong khac");
                    }
                    else
                    {
                        Console.WriteLine($"Phong {room.RoomNumber} con trong.");
                       
                            while (true)
                            {
                                Console.WriteLine("\n==================|Dat Phong|===================");
                                Console.WriteLine("|            1. Dat Phong.                     |");
                                Console.WriteLine("|            2. Thoat.                         |");
                                Console.WriteLine("================================================");

                                string choice = Console.ReadLine();
                                switch (choice)
                                {
                                    case "1": orderRoom(); break;
                                    case "2": return;
                                }
                            }
                        
                    }
                }
                else
                {
                    Console.WriteLine("khong co phong va khach can tim.");
                }
            }
        }
        public void orderRoom()
        {
            Console.WriteLine("\nDanh sach cac phong:");
            Console.WriteLine("\nSTT | Ten phong | Trang thai | Gia tien | Trong/Da su dung");
            int stt = 1;
            foreach (var room in rooms)
            {
                string status = room.IsOccupied ? "Da su dung" : "Trong";
                Console.WriteLine($"{stt}   | Phong {room.RoomNumber}   | {(room.clean ? "Can don dep" : "San sang")}   | {room.price}     | {status}");
                stt++;
            }
            Console.WriteLine("chon phong can dat.");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room != null && !room.IsOccupied)
                {
                    Console.WriteLine($"chon ngay dat phong.");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                    {
                        if (date > DateTime.Now)
                        {
                            room.OrdDate = date;
                            Console.WriteLine($"phong: {room.RoomNumber} duoc dat vao ngay {room.OrdDate}");
                        }
                        else
                        {
                            Console.WriteLine("vui long chon ngay lon hon ngay hien tai.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ngay khong hop le.");
                    }
                }
                else
                {
                    Console.WriteLine("phong khong ton tai hoac da co nguoi su dung.");
                }
            }
        }
        public void ordClean()
        {
            Console.WriteLine("Nhap so phong can 'Can don dep'  ");
            if (int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
                if (room != null)
                {
                    room.clean = true;
                    Console.WriteLine($"Phong : {room.RoomNumber} da duoc danh dau can don dep.");
                }
                else
                {
                    Console.WriteLine("so phong khong hop le.");
                }
            }
        }
    }
}