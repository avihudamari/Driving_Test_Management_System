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
    /// Interaction logic for AddTester.xaml
    /// </summary>
    public partial class AddTester : Window
    {
        IBL bl = FactoryBL.GetInstance();
        Tester tester = new Tester();
        public AddTester()
        {
            InitializeComponent();
            addtestergrid.DataContext = tester;
            testerGender.ItemsSource= Enum.GetValues(typeof(Gender));
            testerVehicle.ItemsSource= Enum.GetValues(typeof(VehicleType));
            birthDate.SelectedDate = DateTime.Now.AddYears(-(Configuration.MIN_TESTER_AGE));
        }
        public void Add_Tester_Button(object sender, RoutedEventArgs e)
        {
            addSchedule();
            try
            {
                bl.addTester(tester);
                MessageBox.Show("The tester was successfully added");
                this.Close();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "ERROR");
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
