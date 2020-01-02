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
    /// Interaction logic for traineeWindow.xaml
    /// </summary>
    public partial class traineeWindow : Window
    {
        IBL bl = FactoryBL.GetInstance();
        Trainee trainee = new Trainee();
        public traineeWindow(Trainee t)
        {
            InitializeComponent();
            welcomelabel.Content = "Welcome! " + t.PrivateName + " " + t.FamilyName;
            if (t.passedTheTest)
            {
                welcomeBlock.Text = "Congratulations! you passed the test on " + t.TraineeGearbox + " " + t.TraineeVehicle + " at " + t.TestDay.ToShortDateString();
            }//if the trainee passed
            else
            {
                if (t.TestDay < DateTime.Now)
                {
                    welcomeBlock.Text = "you haven't passed the test yet on "  + t.TraineeGearbox + " " + t.TraineeVehicle;
                }//if the traine didn't pass the test
                else
                {
                    welcomeBlock.Text = "your next test is on: " + t.TestDay.ToString();
                    testButton.Visibility = Visibility.Hidden;
                }//if he has a test in the future
            }
            trainee = t;
            addtraineegrid.DataContext = trainee;
            traineeGender.ItemsSource = Enum.GetValues(typeof(Gender));
            traineeGearbox.ItemsSource = Enum.GetValues(typeof(GearBox));
            traineeVehicle.ItemsSource = Enum.GetValues(typeof(VehicleType));
            this.Closing += this.On_Closed;
        }
        private void On_Closed(Object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void editInfo(object sender, RoutedEventArgs e)
        {
            updateButton.Content = "update Details";
            updateButton.Click -= editInfo;
            updateButton.Click += updateDetails;
            privateName.IsEnabled = true;
        }
        private void updateDetails(object sender, RoutedEventArgs e)
        {
            updateButton.Content = "edit Info";
            updateButton.Click -= updateDetails;
            updateButton.Click += editInfo;
            privateName.IsEnabled = false;
            try
            {
                bl.updateTrainee(trainee);
            }
            catch (Exception e1)
            {
                MessageBox.Show("trainee info couldn't be updated due to:\n"+e1.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool found = false;
            foreach (var item in bl.getAllTests())
            {
                if (item.TraineeId == trainee.id)
                {
                    found = true;
                    testUpdateWindow testInfo = new testUpdateWindow(item);
                    testInfo.IsEnabled = false;
                    testInfo.ShowDialog();
                    break;
                }
            }
            if (!found)
                MessageBox.Show("no test exist to this trainee");
            
        }
    }
}
