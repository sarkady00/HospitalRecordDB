using HospitalRecord.ViewModel;
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

namespace HospitalRecord.View
{
    /// <summary>
    /// Interaction logic for AddPatientWindow.xaml
    /// </summary>

    public partial class AddPatientWindow : Window
    {
        
        private bool winLoaded = false; // Monitor if the window is loaded

        public AddPatientWindow()
        {
            InitializeComponent();

            // Set the Context's path to the corresponding ViewModel
            AddPatientWindowViewModel addPatientWindow = new AddPatientWindowViewModel();
            this.DataContext = addPatientWindow;

        }

        // Change UI on ComboBox selection change
        private void AddRemovePatientComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = ((sender as ComboBox).SelectedItem as ComboBoxItem).Content as string;

            if (winLoaded)
            {
                if (text == "Felvétel")
                {
                    registerGrid.Visibility = Visibility.Visible;
                    removeGrid.Visibility = Visibility.Hidden;
                }
                else if (text == "Hazaenged")
                {
                    removeGrid.Visibility = Visibility.Visible;
                    registerGrid.Visibility = Visibility.Hidden;
                }
            }
        }

        // Change UI on ComboBox selection change
        private void TypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem.ToString();

            if (text == "Fekvő")
            {
                RoomLabel.IsEnabled = true;
                RoomField.IsEnabled = true;
            }
            else if (text == "Járó")
            {
                RoomLabel.IsEnabled = false;
                RoomField.IsEnabled = false;
                RoomField.Clear();
            }
        }

        // UI logic to clear the TextBoxes
        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500); // wait for field validation to be done

            if (AddPatientWindowViewModel.isValid)
            {
                AddPatientWindowViewModel.isValid = false;
                AddPatientWindowViewModel.isValidationTriggered = false;
                NameField.Clear();
                BDateField.Clear();
                IDnumbField.Clear();
                AddressField.Clear();
                HealthCareNumbField.Clear();
                RoomField.Clear();

                // Initialize again the MainWindowViewModel and therefore the patient list of MainWindow with the new data
                MainWindowViewModel window = new MainWindowViewModel();
                
            }

        }

        // UI logic to clear the ID TextBox
        private void RemovePatient_Click(object sender, RoutedEventArgs e)
        {
            IDField.Clear();
        }

        // UI logic to set Window layout elements at Window load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string text = ((ManagePatientCombo as ComboBox).SelectedItem as ComboBoxItem).Content as string;
            winLoaded = true;

            AddPatientWindowViewModel.isValidationTriggered = true;


            if (text == "Felvétel")
            {
                registerGrid.Visibility = Visibility.Visible;
                removeGrid.Visibility = Visibility.Hidden;
            }
            else if (text == "Törlés")
            {
                removeGrid.Visibility = Visibility.Visible;
                registerGrid.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            winLoaded = false;
            AddPatientWindowViewModel.isValidationTriggered = false;
        }
    }
}
