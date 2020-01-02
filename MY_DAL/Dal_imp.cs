using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MY_DAL
{
    public class Dal_imp:Idal
    {
        public Dal_imp()
        {
            DS.DataSource.testersList = new List<MY_BE.Tester>();
            DS.DataSource.testsList = new List<MY_BE.Test>();
            DS.DataSource.traineesList = new List<MY_BE.Trainee>();
        }
        public int testid;
        public void addTester(MY_BE.Tester tester)
        {
            DS.DataSource.testersList.Add(tester);
        }
        public void removeTester(MY_BE.Tester tester)
        {
            DS.DataSource.testersList.Remove(tester);
        }
        public void updateTester(MY_BE.Tester tester)
        {
            bool flag = false;
            MY_BE.Tester t = new MY_BE.Tester();
            foreach(MY_BE.Tester item in DS.DataSource.testersList)
            {
                if (item.id == tester.id)
                {
                    t = item;
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                DS.DataSource.testersList.Remove(t);
                DS.DataSource.testersList.Add(tester);
            }
            else
                throw new Exception("tester not found");
            
        }
        public void addTrainee(MY_BE.Trainee trainee)
        {
            DS.DataSource.traineesList.Add(trainee);
        }
        public void removeTrainee(MY_BE.Trainee trainee)
        {
            DS.DataSource.traineesList.Remove(trainee);
        }
        public void updateTrainee(MY_BE.Trainee trainee)
        {
            bool flag = false;
            MY_BE.Trainee t = new MY_BE.Trainee();
            foreach (MY_BE.Trainee item in DS.DataSource.traineesList)
            {
                if (item.id == trainee.id)
                {
                    t = item;
                    flag = true;
                    break;
                }
            }
            if(flag)
            {
                DS.DataSource.traineesList.Remove(t);
                DS.DataSource.traineesList.Add(trainee);
            }
            else
                throw new Exception("trainee not found");
        }
         
        public void addTest(MY_BE.Test test)
        {         
            DS.DataSource.testsList.Add(test);
        }
        public void updateTestOnFinish(MY_BE.Test test)
        {
            bool flag = false;
            MY_BE.Test t = new MY_BE.Test();
            foreach(MY_BE.Test item in DS.DataSource.testsList)
            {
                if (item.TestNumber == test.TestNumber)
                {
                    t = item;
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                DS.DataSource.testsList.Remove(t);
                DS.DataSource.testsList.Add(test);
            }
            else
                throw new Exception("test not found");
        }
        public List<MY_BE.Tester> getAllTesters()
        {
            return DS.DataSource.testersList;
        }    //**need to return a copy. not areference that can change the source
        public List<MY_BE.Trainee> getAllTrainees()
        {
            return DS.DataSource.traineesList;
        }  //**need to return a copy. not areference that can change the source
        public List<MY_BE.Test> getAllTests()
        {
            return DS.DataSource.testsList;
        }        //**need to return a copy. not areference that can change the source
    }
}
