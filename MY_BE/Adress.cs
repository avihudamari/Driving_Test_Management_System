using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MY_BE
{
    public class Adress
    {
        public Adress()
        {
            City = "Jerusalem";
            Street = "Ha-Va'ad ha-Le'umi";
            HouseNumber = 21;
        }
        public Adress(string city= "Jerusalem", string street = "Ha-Va'ad ha-Le'umi", int housenumber = 21)
        {
            City = city;
            Street = street;
            HouseNumber = housenumber;
        }

        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        public override string ToString()
        {
            return Street + " " + HouseNumber.ToString() + " st. " + City;
        }
    }
}
