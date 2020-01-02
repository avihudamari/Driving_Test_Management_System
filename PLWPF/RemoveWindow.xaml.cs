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
    /// Interaction logic for RemoveWindow.xaml
    /// </summary>
    public partial class RemoveWindow : Window
    {
        IBL bl = FactoryBL.GetInstance();
        public RemoveWindow(object obj)
        {
            InitializeComponent();
            if(obj.GetType() == new Tester().GetType())
            {
                comboBox.ItemsSource = bl.getAllTesters().Select(x => x.id);
                removeButton.Content = "remove tester";
                removeButton.Click += removeTester_Click;
            }
            else
            {
                comboBox.ItemsSource = bl.getAllTrainees().Select(x => x.id);
                removeButton.Content = "remove trainee";
                removeButton.Click += removeTrainee_Click;
            }
        }
        private void removeTester_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(comboBox.SelectedItem.ToString(), out id))
            {
                Tester tester = new Tester();
                tester = bl.getAllTesters().Where(x => x.id == id).FirstOrDefault();                
                try
                {
                    bl.removeTester(tester);
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            else
            {
                MessageBox.Show("must choose a tester");
            }
            this.Close();
        }
        private void removeTrainee_Click(object sender, RoutedEventArgs e)
        {
            int id;
            if (int.TryParse(comboBox.SelectedItem.ToString(), out id))
            {
                Trainee trainee = new Trainee();
                trainee = bl.getAllTrainees().Where(x => x.id == id).FirstOrDefault();
                try
                {
                    bl.removeTrainee(trainee);
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
            else
            {
                MessageBox.Show("must choose a trainee");
            }
            this.Close();
        }
        private void comboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(removeButton.Content.ToString()== "remove tester")
                {
                    removeTester_Click(sender, new RoutedEventArgs());
                }
                else if(removeButton.Content.ToString()== "remove trainee")
                {
                    removeTrainee_Click(sender, new RoutedEventArgs());
                }
            }
        }
    }
}
