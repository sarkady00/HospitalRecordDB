using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalRecord.Model
{
    public class ManagePeople
    {
        // Initiate Data Base Control
        public static PeopleDataBaseControl dbControl = new PeopleDataBaseControl();


        //--------------------------------------------------------- Manage Patients

        public static bool patientRemoved = false;

        // Accessable patients collection
        public static ObservableCollection<Patient> patients = new ObservableCollection<Patient>();

        // Constructor
        public ManagePeople()
        {
        }

        public static ObservableCollection<Patient> GetPatients()
        {
            patients = dbControl.GetPatientsData();
            return patients;
        }

        public static void AddPatient(Patient patient)
        {
            dbControl.AddPatientData(patient);
        }


        public static void RemovePatient(string patientId)
        {

            for (int i = 0; i < patients.Count; i++)
            {
                if (patientId == patients[i].IDnumb)
                {
                    dbControl.RemovePatientData(patientId);
                    patientRemoved = true;
                }
            }

            if (patientRemoved)
            {
                MessageBox.Show("Beteg eltávolítva!", "Sikeres művelet!", MessageBoxButton.OK, MessageBoxImage.Information);
                patientRemoved = false;
            }
            else MessageBox.Show("Nincs ilyen beteg!", "Sikertelen művelet!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Reset only the hardcoded patients
        public static void ResetPatients()
        {
            dbControl.ClearPatientsData();
            dbControl.AddPatientData(new Patient() { Name = "Márton Móric", Address = "Budapest, XI. ker., József Attila 5.", Sex = sex.Férfi, BirthDate = "2001.02.02", Room = 1, IDnumb = "123456AB", HealthCareNumber = "123 456 789", Type = patientType.Fekvő });
            dbControl.AddPatientData(new Patient() { Name = "Példa Pál", Address = "Érd, Alsó 5.", Sex = sex.Férfi, BirthDate = "1965.01.01", IDnumb = "456123AB", HealthCareNumber = "456 789 123", Type = patientType.Járó });
        }


        //--------------------------------------------------------- Manage Employees
        
        public static bool employeeRemoved = false;

        // Accessable employees collection
        public static ObservableCollection<Employee> employees = new ObservableCollection<Employee>();


        public static ObservableCollection<Employee> GetEmployees()
        {
            employees = dbControl.GetEmployeesData();
            return employees;
        }

        public static void AddEmployee(Employee employee)
        {
            dbControl.AddEmployeeData(employee);
        }

        public static void RemoveEmployee(string idNumb)
        {
            for (int i = 0; i < employees.Count; i++)
            {
                if (idNumb == employees[i].IDnumb)
                {
                    dbControl.RemoveEmployeeData(idNumb);
                    employeeRemoved = true;
                }
            }
            if (employeeRemoved)
            {
                MessageBox.Show("Alkalmazott eltávolítva!", "Sikeres művelet!", MessageBoxButton.OK, MessageBoxImage.Information);
                employeeRemoved = false;
            }
            else MessageBox.Show("Nincs ilyen alkalmazott!", "Sikertelen művelet!", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }

        // Reset only the hardcoded employees
        public static void ResetEmployees()
        {
            dbControl.ClearEmployeesData();
            dbControl.AddEmployeeData(new Employee() { Name = "Nagy András", BirthDate = "1975.03.15", IDnumb = "163319NE", Address = "Szentendre, Bajcsy-Zsilinszky 15.", Sex = sex.Férfi, Specialty = specialty.Sebész, PatientsNumb = 2, Salary = 1000000, Job = job.Orvos });
            dbControl.AddEmployeeData(new Employee() { Name = "Kovács Katalin", BirthDate = "1965.11.12", IDnumb = "123654AC", Address = "Budapest, XII. ker., Petőfi Sándor 48.", Sex = sex.Nő, Salary = 0, Job = job.Ápoló, Specialty = specialty.Általános});
            
        }
    }
}
