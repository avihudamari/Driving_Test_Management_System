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
using System.Windows.Shapes;
using MY_BL;
using MY_BE;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        IBL bl = FactoryBL.GetInstance();
        public Administrator()
        {
            InitializeComponent();
            Closing += On_Closed;
            tests_list.ItemsSource = bl.getAllTests();
        }       
        private void On_Closed(Object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private void add_tester(object sender, RoutedEventArgs e)
        {
            AddTester mytester = new AddTester();
            mytester.ShowDialog();
            testers_list.Items.Refresh();
        }//call the add tester window
        private void add_trainee(object sender, RoutedEventArgs e)
        {
            addTrainee addtrainee = new addTrainee();
            addtrainee.ShowDialog();
            trainees_list.Items.Refresh();
        }//call the add trainee window
        private void add_test(object sender, RoutedEventArgs e)
        {
            addTest addtest = new addTest();
            addtest.ShowDialog();
            tests_list.Items.Refresh();
        }//call the add test window
        private void refresh_tests(object sender, MouseButtonEventArgs e)
        {
             tests_list.ItemsSource = bl.getAllTests();
        }//refreshing the tab
        private void refresh_trainees(object sender, MouseButtonEventArgs e)
        {
            trainees_list.ItemsSource = bl.getAllTrainees();
        }//refreshing the tab
        private void refresh_testers(object sender, MouseButtonEventArgs e)
        {
            testers_list.ItemsSource = bl.getAllTesters();
        }//refreshing the tab
        private void Remove_tester(object sender, RoutedEventArgs e)
        {
            RemoveWindow rw = new RemoveWindow(new Tester());
            rw.ShowDialog();
            testers_list.Items.Refresh();
        }
        private void Remove_trainee(object sender, RoutedEventArgs e)
        {
            RemoveWindow rw = new RemoveWindow(new Trainee());
            rw.ShowDialog();
            trainees_list.Items.Refresh();
        }
    }
}
