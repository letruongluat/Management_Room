using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Room
{
    public class Guest
    {
        public string Name { get; private set; }
        public string Gender { get; private set; }
        public int Age { get; private set; }

        public Guest(string name, string gender, int age)
        {
            Name = name;
            Gender = gender;
            Age = age;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Khach hang: {Name} | Gioi tinh: {Gender} | Do tuoi: {Age}");
        }
    }
}
