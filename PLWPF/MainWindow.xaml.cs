using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MY_BL;
using MY_BE;

namespace PLWPF
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl = FactoryBL.GetInstance();
        public MainWindow()
        {
            InitializeComponent();
            comboBoxTesterID.ItemsSource = bl.getAllTesters().Select(x=> x.id);
            comboBoxTraineeId.ItemsSource = bl.getAllTrainees().Select(x => x.id);
        }
        private void adminClick(object sender, RoutedEventArgs e)
        {
            if(AdminUsername.Text=="admin" && AdminPassword.Password == "admin")
            {
                Administrator admin = new Administrator();
                admin.Show();
                this.Close();
            }
        }//admin signin window
        private void testerClick(object sender, RoutedEventArgs e)
        {
            int tid;
            if (int.TryParse(comboBoxTesterID.SelectedItem.ToString(), out tid))//check the user entered a number
            {
                Tester tester = bl.getAllTesters().Where(x => x.id == tid).FirstOrDefault();
                testerwindow testerw = new testerwindow(tester);    
                testerw.Show();    
                this.Close();    
            }
            else
            {
                MessageBox.Show("must choose tester","ERROR!");
            }
        }//tester signin window
        private void traineeClick(object sender, RoutedEventArgs e)
        {
            int tid;
            if (int.TryParse(comboBoxTraineeId.SelectedItem.ToString(), out tid))//check the user entered a number
            {
                Trainee trainee = bl.getAllTrainees().Where(item => item.id == tid).FirstOrDefault();              
                traineeWindow traineew = new traineeWindow(trainee);    
                traineew.Show();    
                this.Close();                   
            }
            else
            {
                MessageBox.Show("must enter number in tester id!", "ERROR!");
            }
        }//trainee signin window
        private void testerId_EnterButton(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                testerClick(this, new RoutedEventArgs());
            }
            
        }
        private void traineeid_EnterButton(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                traineeClick(this, new RoutedEventArgs()); ;
            }
        }
        private void AdminUsername_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.Focus(AdminPassword);
            }
        }
        private void AdminPassword_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                adminClick(sender, e);
            }
        }
    }
}
