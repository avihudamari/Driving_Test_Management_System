using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using System.Threading.Tasks;
using MY_BE;
using System.Xml;

namespace MY_DAL
{
    class Dal_XML_imp : Idal
    {
        XElement testersroot;
        XElement testsroot;
        XElement traineesroot;
        XElement configroot;
        string testerspath = @"testersXml.xml";
        string testspath = @"testsXml.xml";
        string traineespath = @"traineesXml.xml";
        string configpath = @"configXml.xml";
        public List<Test> tests = new List<Test>();
        public List<Tester> testers = new List<Tester>();
        public List<Trainee> trainees = new List<Trainee>();
        public Dal_XML_imp()
        {
            if (File.Exists(configpath))
            {
                configroot = XElement.Load(configpath);
                Configuration.TEST_ID = int.Parse(configroot.Element("TEST_ID").Value);
            }
            else
            {
                configroot = new XElement("config",new XElement("TEST_ID",Configuration.TEST_ID));
                configroot.Save(configpath);
            }
            if (File.Exists(testerspath))
            {
                testersroot = XElement.Load(testerspath);
                testers = LoadFromXML<List<Tester>>(testerspath);
            }
            else
            {
                testersroot = new XElement("testers");
                //testersroot.Save(testerspath);
                testers = new List<Tester>();
            }
            if (File.Exists(testspath))
            {
                testsroot = XElement.Load(testspath);
                foreach(var el in testsroot.Elements())
                {
                    tests.Add(XmlToTest(el));
                }
            }
            else
            {
                testsroot = new XElement("tests");
                testsroot.Save(testspath);
                tests = new List<Test>();
            }
            if (File.Exists(traineespath))
            {
                traineesroot = XElement.Load(traineespath);
                trainees = LoadFromXML<List<Trainee>>(traineespath);
            }
            else
            {
                traineesroot = new XElement("trainees");
                //traineesroot.Save(traineespath);
                trainees = new List<Trainee>();
            }
        }
        public void addTester(MY_BE.Tester tester)
        {
            testers.Add(tester);
            SaveToXML(testers, testerspath);
        }
        public void removeTester(MY_BE.Tester tester)
        {
            //testers = LoadFromXML<List<Tester>>(testerspath);
            testers.Remove(tester);
            SaveToXML(testers, testerspath);
        }
        public void updateTester(MY_BE.Tester tester)
        {
            //testers = LoadFromXML<List<Tester>>(testerspath);
            Tester t = new Tester();
            foreach (MY_BE.Tester item in testers)
            {
                if (item.id == tester.id)
                {
                    t = item;
                    break;
                }
            }
            testers.Remove(t);
            testers.Add(tester);
            SaveToXML(testers, testerspath);
        }
        public void addTrainee(MY_BE.Trainee trainee)
        {
            trainees.Add(trainee);
            SaveToXML(trainees, traineespath);
        }
        public void removeTrainee(MY_BE.Trainee trainee)
        {
            //trainees = LoadFromXML<List<Trainee>>(traineespath);
            trainees.Remove(trainee);
            SaveToXML(trainees, traineespath);
        }
        public void updateTrainee(MY_BE.Trainee trainee)
        {
            //trainees = LoadFromXML<List<Trainee>>(traineespath);
            Trainee t = new Trainee();
            foreach(Trainee item in trainees)
            {
                if (item.id == trainee.id)
                {
                    t = item;
                }
            }
            trainees.Remove(t);
            trainees.Add(trainee);
            SaveToXML(trainees, traineespath);
        }
        public void addTest(MY_BE.Test test)
        {
            tests.Add(test);
            //testsroot = XElement.Load(testspath);
            XElement xelement = new XElement("test",
                                    new XElement("TesterId", test.TesterId),
                                    new XElement("TraineeId", test.TraineeId),
                                    new XElement("TestAdress", 
                                    new XElement("City",test.TestAdress.City),
                                    new XElement("Street", test.TestAdress.Street),
                                    new XElement("HouseNumber", test.TestAdress.HouseNumber)),
                                    new XElement("TesterNote", test.TesterNote),
                                    new XElement("TestNumber", test.TestNumber),
                                    new XElement("TraineeVehicle", test.TraineeVehicle),
                                    new XElement("TestDate", test.TestDate),
                                    new XElement("BoolTestParams", test.BoolTestParams.Select(x => new XElement("dict", 
                                    new XElement("Key", x.Key),new XElement("Value",x.Value)))),
                                    new XElement("TestParams", test.TestParams.Select(x => new XElement("dict",
                                    new XElement("Key", x.Key), new XElement("Value", x.Value)))),
                                    new XElement("isCheacked", test.isCheacked)
                            );
            testsroot.Add(xelement);
            testsroot.Save(testspath);
            configroot=new XElement("config",new XElement("TEST_ID", Configuration.TEST_ID));
            configroot.Save(configpath);
        }
        public void updateTestOnFinish(MY_BE.Test test)
        {
            Test t = new Test();
            foreach (Test item in tests)
            {
                if (item.TestNumber == test.TestNumber)
                {
                    t = item;
                    break;
                }
            }
            tests.Remove(t);
            tests.Add(test);
            testsroot = new XElement("tests");
            foreach(Test item in tests)
            {
                testsroot.Add(new XElement("test",
                                    new XElement("TesterId", item.TesterId),
                                    new XElement("TraineeId", item.TraineeId),
                                    new XElement("TestAdress",
                                    new XElement("City", item.TestAdress.City),
                                    new XElement("Street", item.TestAdress.Street),
                                    new XElement("HouseNumber", item.TestAdress.HouseNumber)),
                                    new XElement("TesterNote", item.TesterNote),
                                    new XElement("TestNumber", item.TestNumber),
                                    new XElement("TraineeVehicle", item.TraineeVehicle),
                                    new XElement("TestDate", item.TestDate),
                                    new XElement("BoolTestParams", item.BoolTestParams.Select(x => new XElement("dict",
                                    new XElement("Key", x.Key), new XElement("Value", x.Value)))),
                                    new XElement("TestParams", item.TestParams.Select(x => new XElement("dict",
                                    new XElement("Key", x.Key), new XElement("Value", x.Value)))),
                                    new XElement("isCheacked", item.isCheacked)
                            ));
            }
            
            testsroot.Save(testspath);//run over the existing file
        }
        public List<MY_BE.Tester> getAllTesters()
        {
            return testers;
        }
        public List<MY_BE.Trainee> getAllTrainees()
        {
            return trainees;
        }
        public List<MY_BE.Test> getAllTests()
        {
            return tests;
        }
        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }
        public static Test XmlToTest(XElement element)
        {
            Dictionary<string, bool> BoolTParams = new Dictionary<string, bool>();
            Dictionary<string, Rating> TParams = new Dictionary<string, Rating>();
            //IEnumerable<XElement> a = element.Element("BoolTestParams").Elements();

            foreach (var el in element.Element("BoolTestParams").Elements())
            {
                //el.Element("Key");
                //string s = el.Element("Key").Value;
                //bool r = bool.Parse(el.Element("Value").Value);
                BoolTParams.Add(el.Element("Key").Value, bool.Parse(el.Element("Value").Value));
            }
            foreach (var el in element.Element("TestParams").Elements())
            {
                TParams.Add(el.Element("Key").Value, (Rating)Enum.Parse(new Rating().GetType(), el.Element("Value").Value));
            }
            Test test = new Test()
            {
                isCheacked = bool.Parse(element.Element("isCheacked").Value),
                TestNumber = element.Element("TestNumber").Value,
                TesterId = int.Parse(element.Element("TesterId").Value),
                TraineeId = int.Parse(element.Element("TraineeId").Value),
                TestAdress = new Adress(element.Element("TestAdress").Element("City").Value,
                element.Element("TestAdress").Element("Street").Value,
                int.Parse(element.Element("TestAdress").Element("HouseNumber").Value)),
                TesterNote = element.Element("TesterNote").Value,
                TraineeVehicle = (VehicleType)Enum.Parse(new VehicleType().GetType(), element.Element("TraineeVehicle").Value),
                TestDate = DateTime.Parse(element.Element("TestDate").Value),
                BoolTestParams = BoolTParams,
                TestParams = TParams
            };
            return test;
        }
    }
}
