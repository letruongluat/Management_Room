using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Room
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Room room = new Room();
            
           
                while (true)
                {
                   
                    Console.WriteLine("\n===================| Đang Nhap |======================");
                    Console.WriteLine("|               1. Quan Tri Vien.                     |");
                    Console.WriteLine("|               2. Khach Hang.                        |");
                    Console.WriteLine("|               3. Thoat.                             |");
                    Console.WriteLine("|               vui long chon so!!                    |");
                    Console.WriteLine("=======================================================");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1": room.LoginADM(); break;
                        case "2": room.LoginGuest(); break;
                        case "3": return;
                    }
                }
            
        }
    }
}
