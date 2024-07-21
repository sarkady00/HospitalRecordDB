using HospitalRecord.Commands;
using HospitalRecord.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace HospitalRecord.ViewModel
{
    public class AddPatientWindowViewModel : IDataErrorInfo
    {
        // Button Commands
        public ICommand AddPatientCommand { get; set; }
        public ICommand RemovePatientCommand { get; set; }

        // Properties
        public string Name { get; set; }
        public string BirthDate { get; set; }
        private string idNumb;
        public string IDnumb { get { return idNumb; } set { idNumb = value.ToUpper(); } }
        public string Address { get; set; }
        public sex Sex { get; set; }

        private string hcNumb;
        public string HealthCareNumber
        {
            get { return hcNumb; }

            set
            {
                // Format text to separate the numbers by space into groups of three
                string pattern = @"(\d{3}) ?(\d{3}) ?(\d{3})";
                string replacement = "$1 $2 $3";

                hcNumb = Regex.Replace(value, pattern, replacement);
            }
        }

        public int? Room { get; set; }
        public patientType Type { get; set; }

        // Enum arrays for ComboBox item binding
        public Array SexEnumArray
        {
            get { return Enum.GetValues(typeof(sex)); }
        }

        public Array TypeEnumArray
        {
            get { return Enum.GetValues(typeof(patientType)); }
        }



        // Constructor
        public AddPatientWindowViewModel()
        {
            AddPatientCommand = new RelayCommand(AddPatientMethod, CanAddPatientMethod);
            RemovePatientCommand = new RelayCommand(RemovePatientMethod, CanRemovePatientMethod);
        }

        // Variables for validation
        private string validationMessage;
        public static bool isValidationTriggered = false;
        public static bool isValid = false;

        // IDataErrorInfo interface implementation for field validation
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                // Do not check validation on Window load
                if (!isValidationTriggered)
                {
                    return null;
                }

                // Validation criterias and errormessages for different properties
                if (columnName == nameof(Name))
                {
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        return "A név nem lehet üres!";
                    }
                    else if (Name.Length > 30) return "Túl hosszú név!";
                }

                if (columnName == nameof(BirthDate))
                {
                    var reg = new Regex(@"^\d{4}.(0[1-9]|1[0-2]).(0[1-9]|[12]\d|3[01]).?$");
                    if (string.IsNullOrEmpty(BirthDate))
                    {
                        return "A dátum nem lehet üres!";
                    }
                    else if (!reg.IsMatch(BirthDate))
                    {
                        return "Nem megfelelő dátumformátum!";
                    }
                }

                if (columnName == nameof(IDnumb))
                {
                    var reg = new Regex(@"^\d{6}[A-Z]{2}$");
                    if (string.IsNullOrEmpty(IDnumb))
                    {
                        return "A szig. szám nem lehet üres!";
                    }
                    else if (!reg.IsMatch(IDnumb))
                    {
                        return "Nem megfelelő \nszig. szám formátum!";
                    }
                }

                if (columnName == nameof(Address))
                {
                    if (string.IsNullOrWhiteSpace(Address))
                    {
                        return "A cím nem lehet üres!";
                    }
                    else if (Address.Length > 50) return "Túl hosszú lakcím!";
                }

                if (columnName == nameof(HealthCareNumber))
                {
                    var reg = new Regex(@"^(\d{3}) ?(\d{3}) ?(\d{3})$");
                    if (string.IsNullOrEmpty(HealthCareNumber))
                    {
                        return "A TAJ szám nem lehet üres!";
                    }
                    else if (!reg.IsMatch(HealthCareNumber))
                    {
                        return "Nem megfelelő \nTAJ szám formátum!";
                    }
                }

                return null;
            }
        }

        // Validate method to check if all requirements are met
        public bool Validate()
        {
            isValidationTriggered = true;

            string[] properties = { nameof(Name), nameof(BirthDate), nameof(IDnumb), nameof(Address), nameof(HealthCareNumber) };
            foreach (string property in properties)
            {
                string error = this[property];
                if (!string.IsNullOrEmpty(error))
                {
                    validationMessage = error;
                    return false;
                }
            }

            validationMessage = null;
            return true;
        }
        

        // Command methods
        private bool CanRemovePatientMethod(object obj)
        {
            return true;
        }

        private void RemovePatientMethod(object obj)
        {
            ManagePeople.RemovePatient(IDnumb);

            // Reinitialize the MainWindowViewModel and therefore the patient list of MainWindow with the new data
            MainWindowViewModel windowViewModel = new MainWindowViewModel();
        }



        private bool CanAddPatientMethod(object obj)
        {
            return true;
        }

        private void AddPatientMethod(object obj)
        {
            if (Validate())
            {
                isValid = true;
                ManagePeople.AddPatient(new Patient() { Name = Name, BirthDate = BirthDate, IDnumb = IDnumb, Address = Address, Sex = Sex, HealthCareNumber = HealthCareNumber, Room = Room, Type = Type });
                MessageBox.Show("Beteg felvéve.", "Sikeres művelet!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else MessageBox.Show(validationMessage, "Sikertelen művelet!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
