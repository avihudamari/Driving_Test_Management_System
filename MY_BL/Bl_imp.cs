using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using MY_BE;
using MY_DAL;
using System.Reflection;

namespace MY_BL
{
    public class Bl_imp:IBL
    {
        Idal d = FactoryDal.GetInstance();//my idal to work with
        public void addTester(Tester tester)
        {
            bool canadd=true;
            string exception = "";
            foreach(Tester item in getAllTesters())
            {
                if (tester.id == item.id)
                {
                    exception += "tester with the same id already exists\n"; //רוצים לזרוק חריגה שתכלול את כל הבעיות
                    canadd = false;
                }
            }
            DateTime today = DateTime.Today;
            int age = today.Year - tester.BirthDate.Year;
            if (tester.BirthDate > today.AddYears(-1*age))
            {
                age--;
            }
            if(age <= Configuration.MIN_TESTER_AGE)
            {
                canadd = false;
                exception += "tried to insert tester younger than" + Configuration.MIN_TESTER_AGE + "\n";
            }
            if (tester.Seniority < Configuration.MIN_TESTER_SENIORITY)
            {
                canadd = false;
                exception += "tester must have at least " + Configuration.MIN_TESTER_SENIORITY + " years of seniority";
            }
            if (tester.id <= 0)
            {
                canadd = false;
                exception += "id must be larger than 0";
            }
            if (canadd)
            {
                d.addTester(tester);
            }
            else
            {
                throw new Exception(exception);
            }
        }
        public void removeTester(MY_BE.Tester tester)
        {
            //if (tester == null)
            //{
            //    throw new Exception("tester is null");
            //}
            bool tester_exists = false;
            foreach(Tester item in getAllTesters())
            {
                if (item.id == tester.id)
                {
                    tester_exists = true;
                }
            }
            if (!tester_exists)
            {
                throw new Exception("tester not found");
            }
            else
            {
                d.removeTester(tester);
            }
        }
        public void updateTester(MY_BE.Tester tester)
        {
            bool tester_exists = false;
            foreach (Tester item in getAllTesters())
            {            
                if (item.id == tester.id)
                {
                    tester_exists = true;
                }
            }
            if (!tester_exists)
            {
                throw new Exception("tried updating a tester not in database");
            }            
                d.updateTester(tester);         
        }
        public void addTrainee(MY_BE.Trainee trainee)
        {
            int traineeBirthYear = trainee.BirthDate.Year;
            bool canaddtrainee = true;
            int currentYear = DateTime.Now.Year;
            string exception = "";
            foreach(Trainee item in getAllTrainees())
            {
                if (trainee.id == item.id)
                {
                    exception += "trainee with the same id already exists\n";
                    canaddtrainee = false;
                }
            }
            if (currentYear - traineeBirthYear < Configuration.MIN_TRAINEE_AGE)
            {
                canaddtrainee = false;
                exception += "tried to insert trainee younger than " + Configuration.MIN_TRAINEE_AGE + "\n";
            }
            if (canaddtrainee)
            {
                d.addTrainee(trainee);
            }
            else
            {
                throw new Exception(exception);
            }
        }
        public void removeTrainee(MY_BE.Trainee trainee)
        {
            bool trainee_exists = false;
            foreach (Trainee item in getAllTrainees())
            {
                if (item.id == trainee.id)
                {
                    trainee_exists = true;                    
                }
            }
            if (!trainee_exists)
            {
                throw new Exception("trainee not found");
            }
            else
            {
                d.removeTrainee(trainee);
            }
        }
        public void updateTrainee(MY_BE.Trainee trainee)
        {
            bool trainee_exists = false;
            foreach (Trainee item in getAllTrainees())
            {
                if (item.id == trainee.id)
                {
                    trainee_exists = true;               
                }
            }
            if (!trainee_exists)
            {
                throw new Exception("tried updating a trainee not in database");
            }
             d.updateTrainee(trainee);
        }
        public void addTest(MY_BE.Test test)
        {
            bool myFlag = true;
            bool canDoTest = true;
            string exception = "";
            Trainee newTrainee = null;
            Tester newTester = null;
            if (test.TestDate.DayOfWeek >= DayOfWeek.Friday)
            {
                canDoTest = false;
                exception += "you cant do a test on the weekend.\n";
            }
            if(test.TestDate.Hour<9|| test.TestDate.Hour >= 15)
            {
                canDoTest = false;
                exception += "the testers work between 09:00-15:00 only.\n";
            }
            if (test.TestDate <= DateTime.Now)
            {
                canDoTest = false;
                exception += "test date must be in the future.\n";
            }
            foreach (var item in getAllTrainees())//check if trainee is valid for test
            {
                if (item.id == test.TraineeId)
                {
                    newTrainee = item;
                    test.TraineeVehicle = item.TraineeVehicle;
                    if (item.TestDay >= DateTime.Now)
                    {
                        canDoTest = false;
                        exception += "cant set test when you have an upcoming test.\n";
                    }//cant set a test when the trainee already have an upcoming test
                     if (item.passedTheTest==true)
                    {
                        canDoTest = false;
                        exception += "The Trainee has already passed the test.\n";
                    }//if the trainee already passed the test
                    if ((test.TestDate - item.TestDay).TotalDays <= Configuration.TEST_TO_TEST_TIME_RANGE)
                    {
                        canDoTest = false;
                        exception+="you need to wait " + Configuration.TEST_TO_TEST_TIME_RANGE + " days between tests.\n";
                    }//a 'Configuration.TEST_TO_TEST_TIME_RANGE' between the last test and current test
                    if (item.DrivingLessonsNumber < Configuration.MIN_CLASS_NUM)
                    {
                        canDoTest = false;
                        exception+= "atleast " + Configuration.MIN_CLASS_NUM + " lessons required to make a test.\n";
                    }//check if the trainee did less than 'Configuration.MIN_CLASS_NUM' lessons
                    break;//only one student with matching id in the list
                }
            }
            if (newTrainee == null)
            {
                throw new Exception( "trainee does not exist.\n");
            }
            foreach (var item in getAllTesters())//check if tester is available for test
            {
                if (!canDoTest)
                {
                    break;
                }//if you cant do the test it wont look for tester
                if (!item.weekdays[test.TestDate.DayOfWeek, test.TestDate.Hour])
                {
                    continue;
                }//check if the test hour is available
                var v = from t in d.getAllTests()
                        where t.TesterId == item.id
                        select t;
                foreach (Test _test in v)
                {
                    if (_test.TestDate == test.TestDate)
                    {
                        myFlag = false;
                    }
                }
                if (!myFlag)
                {
                    continue;
                }//check if the tester not had test in this time 
                if (item.MaxWeeklyTests == item.weekdays.currentWeeklyTests)
                {
                    continue;
                }//check if the tester passed the max weekly tests 
                if (newTrainee.TraineeVehicle != item.TesterVehicle)
                {
                    continue;
                }//check if the tester test on the trainee vehicle
                if (getDistance(test.TestAdress, item.TesterAdress) > item.MaxDistance)
                {
                    continue;
                }//check if the tester is close enough to the test location
                newTester = item;   //if the tester is available for testing assign the tester for the test
                test.TesterId = item.id;
                break;
            }//look for available tester
            if (newTester == null)
            {
                canDoTest = false;
                exception += "no available tester.\n";
            }//check if there was an available tester for the test
            if (canDoTest)
            {
                ++MY_BE.Configuration.TEST_ID;
                test.TestNumber = (MY_BE.Configuration.TEST_ID).ToString("00000000");
                d.addTest(test);
                newTrainee.TestDay = test.TestDate;
                //newTester.weekdays[test.TestDate.DayOfWeek, test.TestDate.Hour] = false;
                newTester.weekdays.currentWeeklyTests++;
                updateTester(newTester);
                updateTrainee(newTrainee);
            }
            else
            {
                throw new Exception(exception);
            }
        }
        public void updateTestOnFinish(MY_BE.Test test)
        {

            if (test.TestDate >= DateTime.Now)
            {
                throw new Exception("the test wasnt conducted yet");
            }//you cant update test before it started

            //if (getAllTests().Where(x => x.TestNumber == test.TestNumber && x.TestParams["distance"] == 0) == null)
            //{
            //    throw new Exception("test already checked!");
            //}//check that the test wasn't checked

            if(test.isCheacked)
            {
                throw new Exception("test already checked!");
            }
            //check that the test wasn't checked


            //if (getAllTrainees().Where(x => x.id == test.TraineeId).First().passedTheTest == true)
            //{
            //    throw new Exception("Trainee already passed the test!");
            //}//check if trainee already passed the test



            bool canupdatetest = true;
            string myException="";
            bool testpassed = true;
            int sum = 0;
            
            foreach (var item in test.TestParams)
            {
                if (item.Value == 0)
                {
                    canupdatetest = false;
                    myException += "you must rate all the fields\n";
                    break;
                }
            }//check if the tester rated all relevant fields
            if (!test.BoolTestParams["stopped"])
            {
                testpassed = false;
            }//if the trainee didnt stop in red light/stop sign, he can't pass the test.
            else
            {
                foreach(var item in test.BoolTestParams)
                {
                    if (item.Value == true)
                    {
                        sum += 10;
                    }
                }
                foreach(var item in test.TestParams)
                {
                    sum += (int)item.Value;
                }
                testpassed = (sum >= 65);
            }//rate the trainee performence in the test and grade him
            if (canupdatetest)
            {
                //Tester tester = getAllTesters().Where(x => x.id == test.TesterId).First();
                //tester.weekdays[test.TestDate.DayOfWeek, test.TestDate.Hour] = true;
                //tester.weekdays.currentWeeklyTests--;
                //updateTester(tester);
                test.isCheacked = true;
                d.updateTestOnFinish(test);
            }
            else
            {
                throw new Exception(myException);
            }
            if (testpassed)
            {
                Trainee trainee = getAllTrainees().Where(x => x.id == test.TraineeId).First();
                trainee.passedTheTest = true;
                d.updateTrainee(trainee);
            }
        }//update the test and check if the trainee passed
        public List<MY_BE.Tester> getAllTesters()
        {
            return d.getAllTesters();
        }
        public List<MY_BE.Trainee> getAllTrainees()
        {
            return d.getAllTrainees();
        }
        public List<MY_BE.Test> getAllTests()
        {
            return d.getAllTests();
        }
        public List<Tester> getNearbyTesters(Adress ad)
        {
            List<Tester> tlist = new List<Tester>();           
                foreach (Tester t in getAllTesters())
            {
                if (getDistance(t.TesterAdress, ad) <= t.MaxDistance)
                {
                    tlist.Add(t);
                }
            }
            return tlist;
        }
        public double getDistance(Adress adress1,Adress adress2)
        {
            double distancInKm=10000;
            string origin = adress1.ToString(); //or " אחד העם  פתח תקווה 100 " etc. 
            string destination = adress2.ToString();//or " ז'בוטינסקי  רמת גן 10 " etc. 
            //origin = "pisga 45 st. jerusalem"; //or " אחד העם  פתח תקווה 100 " etc. 
            //destination = "pisga 15 st. jerusalem";//or " ז'בוטינסקי  רמת גן 10 " etc. 
            string KEY = @"ISksQSO4vXWCZ3fUZygGvdX6CJhZREgS";

            string url = @"https://www.mapquestapi.com/directions/v2/route" + @"?key=" + KEY + @"&from=" + origin + @"&to=" + destination + @"&outFormat=xml" + @"&ambiguities=ignore&routeType=fastest&doReverseGeocode=false" + @"&enhancedNarrative=false&avoidTimedConditions=false";

            //request from MapQuest service the distance between the 2 addresses 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close(); //the response is given in an XML format 
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);

            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0") //we have the expected answer 
            {     //display the returned distance     
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                distancInKm = distInMiles * 1.609344;
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402") //we have an answer that an error occurred, one of the addresses is not found 
            {
                throw new Exception("an error occurred, one of the addresses is not found. try again.");
            }
            else //busy network or other error... 
            {
                throw new Exception("We have'nt got an answer, maybe the net is busy...");
            }
            return distancInKm;
        }
        public List<Tester> getTestersAtHour(DateTime dt)
        {
            List<Tester> tlist = new List<Tester>();
            foreach (var item in getAllTesters())
            {
                bool myFlag = true;
                var v = from t in d.getAllTests()
                        where t.TesterId == item.id
                        select t;
                foreach (Test _test in v)
                {
                    if (_test.TestDate == dt)
                    {
                        myFlag = false;
                    }
                }
                if (item.weekdays[dt.DayOfWeek, dt.Hour] && myFlag)
                {
                    tlist.Add(item);
                }
            }
            return tlist;
        }
        public List<Test> testsCheck(Func<Test,bool> func)
        {
            List<Test> tlist = new List<Test>();
            var testWithCondition = from item in getAllTests()
                                    where func(item)
                                    select item;
            return testWithCondition.ToList();
        }//return all the tests that return true for the delegates checks
        public int numOfTests(Trainee trainee)
        {
            int sum = 0;
            foreach(Test item in getAllTests())
            {
                if (item.TraineeId == trainee.id)
                {
                    sum++;
                }
            }
            return sum;
        }
        public bool passedTheTest(Trainee trainee)
        {
            return trainee.passedTheTest;
        }
        public List<Test> testsOnDate(DateTime dt)
        {
            List < Test > tlist= new List<Test>();

            var results = from item in getAllTests()
                          where (dt == item.TestDate)
                          select item;
            foreach (Test item in results)
            {
                tlist.Add(item);
            }
            return tlist;
        }
        public List<Tester> groupByVehicleType()
        {
            List<Tester> tlist = new List<Tester>();
            var results = getAllTesters().GroupBy(item => item.TesterVehicle);
            /*var results2 = from item in getAllTests()
                          group item by item.TraineeVehicle;
            */
            foreach (IGrouping<VehicleType,Tester> group in results)
            {
                //tlist.AddRange(group);
                foreach(Tester item in group)
                {
                    tlist.Add(item);
                }
            }
            return tlist;
        }

        public IEnumerable<IGrouping<string,Trainee>> groupByFeature(string feature)
        {
            PropertyInfo current = typeof(Trainee).GetProperties()
                                            .FirstOrDefault(p => p.Name == feature);

            var result= from tr in getAllTrainees()
                   group tr by (string)current.GetValue(tr,null);
            return result;
        }

        public List<Trainee> groupByDrivingSchool()
        {
            List<Trainee> tlist = new List<Trainee>();
            var results = getAllTrainees().GroupBy(item => item.DrivingSchool);

            foreach (IGrouping<string,Trainee> group in results)
            {
                foreach(Trainee item in group)
                {
                    tlist.Add(item);
                }
            }
            return tlist;
        }
        public List<Trainee> groupByTester()
        {
            List<Trainee> tlist = new List<Trainee>();
            var results = getAllTrainees().GroupBy(item => item.TeacherName);
            foreach (IGrouping<string, Trainee> group in results)
            {
                foreach (Trainee item in group)
                {
                    tlist.Add(item);
                }
            }
            return tlist;
        }//לפי ID ולא שם
        public List<Trainee> groupByTestsNumber()
        {
            List<Trainee> tlist = new List<Trainee>();
            var results = getAllTrainees().GroupBy(item => item.FamilyName);//**need to make tests number field in trainee
            foreach (IGrouping<string, Trainee> group in results)
            {
                foreach (Trainee item in group)
                {
                    tlist.Add(item);
                }
            }
            return tlist;
        }// לחזור
        /*public void addmyTrainees()
        {
            int i, y = -19, num = 25, phone = 0500000000, hnum = 18;
            string name = "n", school = "s";
            char ca = 'a',ca2='z';
            
            DateTime dt = DateTime.Now.AddYears(y);
            for (i = 0; i < 10; i++)
            {
                Trainee trainee = new Trainee();
                trainee.PrivateName = name += ca++; ;
                trainee.FamilyName = name += ca2--;
                trainee.BirthDate = dt.AddYears(y--);
                trainee.DrivingLessonsNumber = num += 2;
                trainee.DrivingSchool = school += "a";
                trainee.id = i;
                trainee.PhoneNumber = (phone += 465).ToString("0000000000");
                trainee.TraineeAdress.City = "jerusalem";
                trainee.TraineeAdress.Street = "havaad hleumi";
                trainee.TraineeAdress.HouseNumber = hnum++;
                trainee.TraineeGearbox = GearBox.Automatic;
                trainee.TraineeGender = Gender.Male;
                trainee.TraineeVehicle = VehicleType.PrivateVehicle;
                try
                {
                    addTrainee(trainee);
                }
                catch (Exception e) { }
            }
        }
        public void addmyTesters()
        {

            int i, y = -41, num = 15, phone = 0520000000, hnum = 18 ,s=10;
            string name = "t";
            DateTime dt = DateTime.Now.AddYears(y);
            for (i = 0; i < 10; i++)
            {
                Tester tester = new Tester();
                tester.PrivateName = name += "a";
                tester.FamilyName = name += "b";
                tester.BirthDate = dt.AddYears(y--);
                tester.MaxDistance = num += 2;
                tester.id = i;
                tester.PhoneNumber = (phone += 236).ToString("0000000000");
                tester.TesterAdress.City = "jerusalem";
                tester.TesterAdress.Street = "bait vagan";
                tester.TesterAdress.HouseNumber = hnum++;
                tester.TesterGender = Gender.Male;
                tester.TesterVehicle = VehicleType.PrivateVehicle;
                tester.MaxWeeklyTests = hnum += 3;
                tester.Seniority = s++;
                try
                {
                    addTester(tester);
                }
                catch (Exception e) { }
            }
        }
        public void addmytests()
        {
            int i,hn=40,teid=10,trid=0;
            for (i = 0; i < 5; i++)
            {
                Test test = new Test();
                test.TestAdress.City = "j-city";
                test.TestAdress.Street = "yafo";
                test.TestAdress.HouseNumber = hn += 4;
                test.TestDate = new DateTime(2019,01,15,12,30,0);
                test.TestDate = test.TestDate;
                test.TesterId = teid--;
                test.TraineeId = trid++;
                test.TraineeVehicle = VehicleType.PrivateVehicle;
                try
                {
                    addTest(test);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }*/
    }

}
