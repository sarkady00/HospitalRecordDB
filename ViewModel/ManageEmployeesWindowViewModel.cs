using HospitalRecord.Commands;
using HospitalRecord.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using HospitalRecord.View;

namespace HospitalRecord.ViewModel
{
    public class ManageEmployeesWindowViewModel : IDataErrorInfo
    {

        public ObservableCollection<Employee> Employees { get; set; }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand RemoveEmployeeCommand { get; set; }

        // Properties 
        public string Name { get; set; }
        public string BirthDate { get; set; }
        private string idNumb;
        public string IDnumb { get { return idNumb; } set { idNumb = value.ToUpper(); } }
        public string Address { get; set; }
        public sex Sex { get; set; }
        public specialty Specialty { get; set; }
        public int? PatientsNumb { get; set; }
        public int? Salary { get; set; }
        public job Job { get; set; }

        // Enum arrays for ComboBox item binding
        public Array SexEnumArray
        {
            get { return Enum.GetValues(typeof(sex)); }
        }

        public Array JobEnumArray
        {
            get { return Enum.GetValues(typeof(job)); }
        }

        public Array SpecialtyEnumArray
        {
            get { return Enum.GetValues(typeof(specialty)); }
        }


        // Constructor
        public ManageEmployeesWindowViewModel()
        {
            UpdateEmployees();

            AddEmployeeCommand = new RelayCommand(AddEmployeeMethod, CanAddEmployeeMethod);
            RemoveEmployeeCommand = new RelayCommand(RemoveEmployeeMethod, CanRemoveEmployeeMethod);
        }

        // Update method
        public void UpdateEmployees()
        {
            Employees = ManagePeople.GetEmployees();

            // Update listview
            //ManageEmployeesWindow.list.ItemsSource = null;
            //ManageEmployeesWindow.list.ItemsSource = Employees;
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

                if (columnName == nameof(Salary))
                {
                    if (!Salary.HasValue)
                    {
                        return "A fizetés nem lehet üres!";
                    }
                }

                return null;
            }
        }

        // Validate method to check if all requirements are met
        public bool Validate()
        {
            isValidationTriggered = true;

            string[] properties = { nameof(Name), nameof(BirthDate), nameof(IDnumb), nameof(Address), nameof(Salary) };
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
        private bool CanRemoveEmployeeMethod(object obj)
        {
            return true;
        }

        private void RemoveEmployeeMethod(object obj)
        {
            ManagePeople.RemoveEmployee(IDnumb);
            UpdateEmployees();
        }



        private bool CanAddEmployeeMethod(object obj)
        {
            return true;
        }

        private void AddEmployeeMethod(object obj)
        {
            if (Validate())
            {
                isValid = true;
                ManagePeople.AddEmployee(new Employee() { Name = Name, BirthDate = BirthDate, IDnumb = IDnumb, Address = Address, Sex = Sex, Specialty = Specialty, PatientsNumb = PatientsNumb, Salary = Salary, Job = Job });
                MessageBox.Show("Alkalmazott felvéve.", "Sikeres művelet!", MessageBoxButton.OK, MessageBoxImage.Information);

                UpdateEmployees();
            }
            else MessageBox.Show(validationMessage, "Sikertelen művelet!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
