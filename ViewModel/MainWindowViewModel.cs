using HospitalRecord.Commands;
using HospitalRecord.Model;
using HospitalRecord.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HospitalRecord.ViewModel
{
    public class MainWindowViewModel
    {

        public ObservableCollection<Patient> Patients { get;set; }

        public ICommand ShowPatientWindowCommand { get; set; }
        public ICommand ShowEmployeeWindowCommand { get; set; }
        public ICommand FactoryResetCommand { get; set; }
        public ICommand ClearDBCommand { get; set; }

        // Constuctor
        public MainWindowViewModel()
        {
            // Initialize collection and commands
            UpdatePatients();

            ShowPatientWindowCommand = new RelayCommand(ShowWindow, CanShowWindow);
            ShowEmployeeWindowCommand = new RelayCommand(ShowEmployees, CanShowEmployees);
            FactoryResetCommand = new RelayCommand(FactoryResetMethod, CanFactoryResetMethod);
            ClearDBCommand = new RelayCommand(ClearDBMethod, CanClearDBMethod);
        }


        // Update method
        public void UpdatePatients()
        {
            Patients = ManagePeople.GetPatients();

            // Update listview
            MainWindow.list.ItemsSource = null;
            MainWindow.list.ItemsSource = Patients;
        }


        // Command methods
        private bool CanShowEmployees(object obj)
        {
            return true;
        }

        private void ShowEmployees(object obj)
        {
            var mainWindow = obj as Window;

            ManageEmployeesWindow manageEmployeesWin = new ManageEmployeesWindow();
            manageEmployeesWin.Owner = mainWindow;
            manageEmployeesWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            manageEmployeesWin.Show();
        }




        private bool CanShowWindow(object obj)
        {
            return true;
        }

        private void ShowWindow(object obj)
        {
            var mainWindow = obj as Window;

            AddPatientWindow addPatientWin = new AddPatientWindow();
            addPatientWin.Owner = mainWindow;
            addPatientWin.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addPatientWin.Show();
        }




        private bool CanClearDBMethod(object obj)
        {
            return true;
        }

        private void ClearDBMethod(object obj)
        {
            // Remove all people
            ManagePeople.dbControl.ClearPatientsData();
            ManagePeople.dbControl.ClearEmployeesData();
            UpdatePatients();
            ManageEmployeesWindowViewModel manageEmployeesWindow = new ManageEmployeesWindowViewModel();
        }




        private bool CanFactoryResetMethod(object obj)
        {
            return true;
        }

        private void FactoryResetMethod(object obj)
        {
            // Reset the stored data and keep only the hardcoded people
            ManagePeople.ResetPatients();
            ManagePeople.ResetEmployees();
            UpdatePatients();
            ManageEmployeesWindowViewModel manageEmployeesWindow = new ManageEmployeesWindowViewModel();
        }
    }
}
