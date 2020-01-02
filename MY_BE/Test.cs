using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace MY_BE
{

    public class Test
    {
        public Test()
        {
            BoolTestParams = new Dictionary<string, bool>();
            TestParams = new Dictionary<string, Rating>();
            TraineeVehicle = VehicleType.PrivateVehicle;
            TestNumber = "0";
            TesterId = 0;
            TraineeId = 0;
            TestDate = new DateTime(2019, 01, 23, 0, 0, 0);
            TestAdress = new Adress();
            TesterNote = "tester note here.";
            BoolTestParams.Add("stopped", false);//add parameters to check if the trainee drove carefuly
            BoolTestParams.Add("parking", false);
            BoolTestParams.Add("pedestrians_row", false);//row=right of way
            TestParams.Add("distance", 0);
            TestParams.Add("mirrors", 0);
            TestParams.Add("turn_signals", 0);
            TestParams.Add("stressed", 0);
            TestParams.Add("tester_breaks", 0);
            TestParams.Add("road_signs", 0);
            TestParams.Add("gradual_stopping", 0);
            TestParams.Add("lane", 0);
            TestParams.Add("road_row", 0);
            isCheacked = false;
        }
        public Test(Test copyTest)
        {
            TestNumber = copyTest.TestNumber;
            TesterId = copyTest.TesterId;
            TraineeId = copyTest.TraineeId;
            TestDate = copyTest.TestDate;
            TraineeVehicle = copyTest.TraineeVehicle;
            TestAdress = copyTest.TestAdress;
            TesterNote = copyTest.TesterNote;
            TestParams = copyTest.TestParams;
            isCheacked = copyTest.isCheacked;
    }

        public VehicleType TraineeVehicle { get; set; }// בשביל הבדיקה הראשונה שהייתי צריך לעשות
        public string TestNumber { get; set; }
        public int TesterId { get; set; }
        public int TraineeId { get; set; }
        public DateTime TestDate { get; set; }
        public Adress TestAdress { get; set; }
        [XmlIgnore]
        public Dictionary<string, Rating> TestParams { get; set; }//make a lot of those!
        [XmlIgnore]
        public Dictionary<string, bool> BoolTestParams { get; set; }
        //params for careful driving, stopping at stop sign,etc...
        public bool isCheacked { get; set; }
        public string TesterNote { get; set; }
        
        
        public string ToString()
        {
            return "";
        }
    }
}
