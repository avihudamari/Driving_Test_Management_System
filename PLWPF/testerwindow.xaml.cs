using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MY_BE;
using MY_BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for testerwindow.xaml
    /// </summary>
    public partial class testerwindow : Window
    {
        IBL bl = FactoryBL.GetInstance();
        Tester tester = new Tester();
        public testerwindow(Tester t)
        {
            tester = t;
            InitializeComponent();
            this.Closing += this.On_Closed;
            List<Test> my_tests = bl.getAllTests().Where(x => x.TesterId == tester.id).ToList();
            scheduledatagrid.ItemsSource = my_tests.Where(x=>x.TestDate >= DateTime.Now);
            testIdComboBox.ItemsSource = my_tests.Where(x => x.TestDate <= DateTime.Now && x.isCheacked == false).Select(x => x.TestNumber);
            welcomeLabel.Content = welcomeLabel.Content + tester.PrivateName+" "+ tester.FamilyName;
            testerGender.ItemsSource = Enum.GetValues(typeof(Gender));
            testerVehicle.ItemsSource = Enum.GetValues(typeof(VehicleType));
            testerinfogrid.DataContext = tester;
        }
        private void On_Closed(Object sender,System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }//when pressing x open the main window
        private void submitButtonClick(object sender, RoutedEventArgs e)
        {
            if (testIdComboBox.SelectedItem != null)
            {
                Test test = bl.getAllTests().Where(x => x.TestNumber == testIdComboBox.SelectedItem.ToString()).FirstOrDefault();
                testUpdateWindow testUp = new testUpdateWindow(test);
                testUp.ShowDialog();
            }
        }//update test grading on pervious tests
        private void tib_Enter_Pressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                submitButtonClick(this, new RoutedEventArgs());
            }
        }
        public void editInfo(object sender, RoutedEventArgs e)
        {
            testerButton.Content = "update Details";
            testerButton.Click -= editInfo;
            testerButton.Click += updateDetails;
            privateName.IsEnabled = true;
            sp1.IsEnabled = true;
            sp2.IsEnabled = true;
            sp3.IsEnabled = true;
            sp4.IsEnabled = true;
            sp5.IsEnabled = true;
        }
        public void updateDetails(object sender, RoutedEventArgs e)
        {

            testerButton.Content = "edit Info";
            testerButton.Click -= updateDetails;
            testerButton.Click += editInfo;
            privateName.IsEnabled = false;
            sp1.IsEnabled = false;
            sp2.IsEnabled = false;
            sp3.IsEnabled = false;
            sp4.IsEnabled = false;
            sp5.IsEnabled = false;
            addSchedule();
            try
            {
                bl.updateTester(tester);
            }
            catch (Exception e1)
            {
                MessageBox.Show("tester info couldn't be updated due to:\n" + e1.Message);
            }
        }
        private void addSchedule()
        {
            //sunday
            tester.weekdays[DayOfWeek.Sunday, 9] = (bool)s0.IsChecked;
            tester.weekdays[DayOfWeek.Sunday, 10] = (bool)s1.IsChecked;
            tester.weekdays[DayOfWeek.Sunday, 11] = (bool)s2.IsChecked;
            tester.weekdays[DayOfWeek.Sunday, 12] = (bool)s3.IsChecked;
            tester.weekdays[DayOfWeek.Sunday, 13] = (bool)s4.IsChecked;
            tester.weekdays[DayOfWeek.Sunday, 14] = (bool)s5.IsChecked;
            //Monday 
            tester.weekdays[DayOfWeek.Monday, 9] = (bool)m0.IsChecked;
            tester.weekdays[DayOfWeek.Monday, 10] = (bool)m1.IsChecked;
            tester.weekdays[DayOfWeek.Monday, 11] = (bool)m2.IsChecked;
            tester.weekdays[DayOfWeek.Monday, 12] = (bool)m3.IsChecked;
            tester.weekdays[DayOfWeek.Monday, 13] = (bool)m4.IsChecked;
            tester.weekdays[DayOfWeek.Monday, 14] = (bool)m5.IsChecked;
            //Tuesday 
            tester.weekdays[DayOfWeek.Tuesday, 9] = (bool)w0.IsChecked;
            tester.weekdays[DayOfWeek.Tuesday, 10] = (bool)w1.IsChecked;
            tester.weekdays[DayOfWeek.Tuesday, 11] = (bool)w2.IsChecked;
            tester.weekdays[DayOfWeek.Tuesday, 12] = (bool)w3.IsChecked;
            tester.weekdays[DayOfWeek.Tuesday, 13] = (bool)w4.IsChecked;
            tester.weekdays[DayOfWeek.Tuesday, 14] = (bool)w5.IsChecked;
            //Wednesday 
            tester.weekdays[DayOfWeek.Wednesday, 9] = (bool)t0.IsChecked;
            tester.weekdays[DayOfWeek.Wednesday, 10] = (bool)t1.IsChecked;
            tester.weekdays[DayOfWeek.Wednesday, 11] = (bool)t2.IsChecked;
            tester.weekdays[DayOfWeek.Wednesday, 12] = (bool)t3.IsChecked;
            tester.weekdays[DayOfWeek.Wednesday, 13] = (bool)t4.IsChecked;
            tester.weekdays[DayOfWeek.Wednesday, 14] = (bool)t5.IsChecked;
            //Thursday
            tester.weekdays[DayOfWeek.Thursday, 9] = (bool)h0.IsChecked;
            tester.weekdays[DayOfWeek.Thursday, 10] = (bool)h1.IsChecked;
            tester.weekdays[DayOfWeek.Thursday, 11] = (bool)h2.IsChecked;
            tester.weekdays[DayOfWeek.Thursday, 12] = (bool)h3.IsChecked;
            tester.weekdays[DayOfWeek.Thursday, 13] = (bool)h4.IsChecked;
            tester.weekdays[DayOfWeek.Thursday, 14] = (bool)h5.IsChecked;
        }
    }
}
