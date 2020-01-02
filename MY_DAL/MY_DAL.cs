using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MY_DAL
{
    public interface Idal
    {//פונקציות בוליאניות בשביל לבדוק שההכנסה עבדה??
        void addTester(MY_BE.Tester tester);
        void removeTester(MY_BE.Tester tester);
        void updateTester(MY_BE.Tester tester);
        void addTrainee(MY_BE.Trainee trainee);
        void removeTrainee(MY_BE.Trainee trainee);
        void updateTrainee(MY_BE.Trainee trainee);
        void addTest(MY_BE.Test test);
        void updateTestOnFinish(MY_BE.Test test);
        List<MY_BE.Tester> getAllTesters();
        List<MY_BE.Trainee> getAllTrainees();
        List<MY_BE.Test> getAllTests();
    }
}
