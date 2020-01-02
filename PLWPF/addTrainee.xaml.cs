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
    /// Interaction logic for addTrainee.xaml
    /// </summary>
    public partial class addTrainee : Window
    {
        IBL bl = FactoryBL.GetInstance();
        Trainee trainee = new Trainee();
        public addTrainee()
        {
            InitializeComponent();
            addtraineegrid.DataContext = trainee;
            traineeGender.ItemsSource = Enum.GetValues(typeof(Gender));
            traineeGearbox.ItemsSource = Enum.GetValues(typeof(GearBox));
            traineeVehicle.ItemsSource = Enum.GetValues(typeof(VehicleType));
            birthDate.SelectedDate = DateTime.Now.AddYears(-(Configuration.MIN_TRAINEE_AGE));
        }
        private void addButton(object sender, RoutedEventArgs e)
        {           
            try
            {
                bl.addTrainee(trainee);
                this.Close();
                MessageBox.Show("The trainee was successfully added");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "ERROR");
            }
        }
    }
}
