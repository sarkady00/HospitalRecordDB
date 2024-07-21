using HospitalRecord.ViewModel;
using System;
using System.Collections;
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

namespace HospitalRecord.View
{
    /// <summary>
    /// Interaction logic for ManageEmployeesWindow.xaml
    /// </summary>
    public partial class ManageEmployeesWindow : Window
    {
        private bool winLoaded = false;
        public static ListView list;

        // Constructor
        public ManageEmployeesWindow()
        {
            InitializeComponent();

            list = EmployeeList;

            ManageEmployeesWindowViewModel employeeWindowViewModel = new ManageEmployeesWindowViewModel();
            this.DataContext = employeeWindowViewModel;
        }

        // CHange UI on ComboBox selection change
        private void addRemoveCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;

            if (winLoaded)
            {
                if (text == "Felvétel")
                {
                    gridAdd.Visibility = Visibility.Visible;
                    gridRemove.Visibility = Visibility.Hidden;
                }
                else if (text == "Törlés")
                {
                    gridRemove.Visibility = Visibility.Visible;
                    gridAdd.Visibility = Visibility.Hidden;
                }
            }
        }

        // Change UI on ComboBox selection change
        private void JobComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem.ToString();

            if (text == "Orvos")
            {
                SpecialtyField.IsEnabled = true;
                PatientsField.IsEnabled = true;
                SpecialtyLabel.IsEnabled = true;
                PatientsLabel.IsEnabled = true;
            }
            else if (text == "Ápoló")
            {
                SpecialtyField.IsEnabled = false;
                PatientsField.IsEnabled = false;
                SpecialtyField.SelectedIndex = 0;
                PatientsField.Clear();
                SpecialtyLabel.IsEnabled = false;
                PatientsLabel.IsEnabled = false;
            }
        }

        // Initialize UI on Window Load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string text = ((addRemoveCombo as ComboBox).SelectedItem as ComboBoxItem).Content as string;
            winLoaded = true;

            ManageEmployeesWindowViewModel.isValidationTriggered = true;

            if (text == "Felvétel")
            {
                gridAdd.Visibility = Visibility.Visible;
                gridRemove.Visibility = Visibility.Hidden;
            }
            else if (text == "Törlés")
            {
                gridRemove.Visibility = Visibility.Visible;
                gridAdd.Visibility = Visibility.Hidden;
            }
        }

        // On Click clear all TextBox
        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);

            if (ManageEmployeesWindowViewModel.isValid)
            {
                ManageEmployeesWindowViewModel.isValid = false;
                ManageEmployeesWindowViewModel.isValidationTriggered = false;
                NameField.Clear();
                BDateField.Clear();
                AddressField.Clear();
                IDnumbField.Clear();
                SalaryField.Clear();
                PatientsField.Clear();
            }
        }

        // On Click clear TextBox
        private void Fire_Click(object sender, RoutedEventArgs e)
        {
            IDField.Clear();
        }

        // On Window Closing reset parameters for later usage
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            winLoaded = false;
            ManageEmployeesWindowViewModel.isValidationTriggered = false;
        }
    }
}
