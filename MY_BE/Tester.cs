using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MY_BE
{
    /*public class Day
    {
        bool[] hour;
        public Day()
        {
            hour = new bool[6];
            for (int i = 0; i < 6; i++)
            {
                hour[i] = true;
            }
        }
        public bool this[int realHour]
        {
            get=> hour[realHour - 9];
            set=> hour[realHour - 9]=value;
        }
    }*/
    public class Schedule
    {
        /*Dictionary<DayOfWeek, Day> week;
        Schedule()
        {
            for (int i = 1; i <= 7; i++)
            {
                
                week.Add(, d);
            }
        }
        public Day this[DayOfWeek d]
        {
            get
            {
                return week[d];
            }
        }*/
        public bool[][] week { get; set; }
        public int currentWeeklyTests { get; set; }

        public Schedule()
        {
            week = new bool[5][];
            for(int i = 0; i < 5; i++)
            {
                week[i] = new bool[6];
                for(int j = 0; j < 6; j++)
                {
                    week[i][j] = true;
                }
            }
            currentWeeklyTests = 0;
        }
        public Schedule(Schedule s)
        {
            currentWeeklyTests = s.currentWeeklyTests;
            week = s.week;
        }
        public bool this[DayOfWeek dof, int h]
        {
            get
            {
                return week[(int)dof][ h - 9];
            }
            set
            {
                week[(int)dof][ h - 9]=value;
            }
        }
        /*public IEnumerator<bool> GetEnumerator()
        {
            
            for(DayOfWeek a = DayOfWeek.Sunday; a <= DayOfWeek.Saturday; a++)
            {
                for(int i=9;i<=15 ;i++)
                {
                    yield return 
                }
            }
        }*/
        
    }
        public class Tester
    {
        public Tester()
        {
            id = 9999;
            FamilyName = "FamilyName";
            PrivateName = "PrivateName";
            BirthDate = DateTime.Now.AddYears(-41);
            TesterGender = Gender.Other;
            PhoneNumber = "050-0000000";
            TesterAdress = new Adress();
            Seniority = 40;
            MaxWeeklyTests = 5 * 6;
            TesterVehicle = VehicleType.PrivateVehicle;
            MaxDistance = 100;
            weekdays = new Schedule();
        }
        public Tester(Tester tester)
        {
            id = tester.id;
            FamilyName = tester.FamilyName;
            PrivateName = tester.PrivateName;
            BirthDate = tester.BirthDate;
            TesterGender = tester.TesterGender;
            PhoneNumber = tester.PhoneNumber;
            TesterAdress = tester.TesterAdress;
            Seniority = tester.Seniority;
            MaxWeeklyTests = tester.MaxWeeklyTests;
            TesterVehicle = tester.TesterVehicle;
            MaxDistance = tester.MaxDistance;
            weekdays = tester.weekdays;
        }


        public int id { get; set; }
        public string FamilyName { get; set; }
        public string PrivateName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender TesterGender { get; set; }
        public string PhoneNumber { get; set; }
        public Adress TesterAdress { get; set; }
        public int Seniority { get; set; }
        public int MaxWeeklyTests { get; set; }   //maybe const?
        public VehicleType TesterVehicle { get; set; }
        public int MaxDistance { get; set; }
        public Schedule weekdays { get; set; }
        public string ToString()
        {
            return "";
        }
        /*public string XmlWeekdays
        {
            set
            {
                if (value != null && value.Length > 0)
                {
                    string[] values = value.Split(',');
                    int sizeA = int.Parse(values[0]);
                    int sizeB = int.Parse(values[1]);
                    int index = 2;
                    for (int i = 0; i < 5; i++)
                        for (int j = 0; j < 6; j++)
                            weekdays.week[i][j] = bool.Parse(values[index++]);
                }
            }
            get
            {
                string result = "";
                    int sizeA = UserMetrix.GetLength(0);
                    int sizeB = UserMetrix.GetLength(1);
                    result += "" + sizeA + "," + sizeB;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            result += "," + UserMetrix[i, j];
                return result;
            }
        }*/
        /*public string nextAvailableTest()
        {
            for (DayOfWeek a = DateTime.Today.DayOfWeek; a <= DayOfWeek.Saturday; a++)
            {
                for (int i = 9; i <= 15; i++)
                {
                    if (weekdays[a, i])
                    {
                        return "next test available in: " + a + " " + i+":00";
                    }
                }
            }

        }*/
    }
}
