using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace HospitalRecord.Model
{
    

    public class PeopleDataBaseControl
    {
        private string connectionString;
        SqlConnection connection;

        public PeopleDataBaseControl()
        {
            connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\sarka\\Desktop\\C# gyakorlás\\HospitalRecordDB\\Model\\DataBasePeople.mdf\";Integrated Security=True";
        }



        //--------------------------------------------------------- Get Employees Data
        public ObservableCollection<Employee> GetEmployeesData()
        {
            string query = "SELECT * FROM Employees";
            using (connection = new SqlConnection(connectionString))
            using(SqlDataAdapter adapter = new SqlDataAdapter(query,connection))
            {
                DataTable employeeTable = new DataTable();
                adapter.Fill(employeeTable);
                ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
                foreach(DataRow row in employeeTable.Rows)
                {
                    // Convert strings to the given enum type
                    string dbSex = (string)row["Sex"];
                    sex Sex;
                    if (Enum.TryParse(dbSex, out sex parsedSex))
                    {
                        Sex = parsedSex;
                    }
                    else
                    {
                        throw new ArgumentException($"Érvénytelen nem: {dbSex}");
                    }

                    string dbJob = (string)row["Job"];
                    job Job;
                    if (Enum.TryParse(dbJob, out job parsedJob))
                    {
                        Job = parsedJob;
                    }
                    else
                    {
                        throw new ArgumentException($"Érvénytelen pozíció: {dbJob}");
                    }

                    string dbSpecialty = (string)row["Specialty"];
                    specialty Specialty;
                    if (Enum.TryParse(dbSpecialty, out specialty parsedSpecialty))
                    {
                        Specialty = parsedSpecialty;
                    }
                    else
                    {
                        throw new ArgumentException($"Érvénytelen szak: {dbSpecialty}");
                    }




                    // Add employee to the collection
                    employees.Add(
                        new Employee()
                        {
                            Name = (string)row["Name"],
                            BirthDate = (string)row["BirthDate"],
                            Age = (int)row["Age"],
                            IDnumb = (string)row["IDnumb"],
                            Address = (string)row["Address"],
                            Sex = Sex,
                            Job = Job,
                            Salary = (int)row["Salary"],
                            Specialty = Specialty,
                            PatientsNumb = row["PatientNumb"] != DBNull.Value ? (int?)row["PatientNumb"] : null
                        }
                    );
                }
                return employees;
            }
        }

        //--------------------------------------------------------- Add Employee Data
        public void AddEmployeeData(Employee emp)
        {
            string query = "INSERT INTO Employees (Name, BirthDate, Age, IDnumb, Address, Sex, Job, Salary, Specialty, PatientNumb) VALUES (@Name, @BirthDate, @Age, @IDnumb, @Address, @Sex, @Job, @Salary, @Specialty, @PatientsNumb)";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query,connection))
            {
                connection.Open();

                command.Parameters.AddWithValue("@Name", emp.Name);
                command.Parameters.AddWithValue("@BirthDate", emp.BirthDate);
                command.Parameters.AddWithValue("@Age", emp.Age);
                command.Parameters.AddWithValue("@IDnumb", emp.IDnumb);
                command.Parameters.AddWithValue("@Address", emp.Address);
                command.Parameters.AddWithValue("@Sex", emp.Sex);
                command.Parameters.AddWithValue("@Job", emp.Job);
                command.Parameters.AddWithValue("@Salary", emp.Salary);
                command.Parameters.AddWithValue("@Specialty", emp.Specialty);
                command.Parameters.AddWithValue("@PatientsNumb", emp.PatientsNumb == null ? (object)DBNull.Value : emp.PatientsNumb);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //--------------------------------------------------------- Remove Employee Data
        public void RemoveEmployeeData(string idnumb)
        {
            string query = "DELETE FROM Employees WHERE IDnumb=@idnumber";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idnumber", idnumb);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                ManagePeople.employeeRemoved = true;
            }
        }

        //--------------------------------------------------------- Clear Employees Data
        public void ClearEmployeesData()
        {
            string query = "DELETE FROM Employees";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }










        //---------------------------------------------------------Get Patients Data
        public ObservableCollection<Patient> GetPatientsData()
        {
            string query = "SELECT * FROM Patients";
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
            {
                DataTable patientsTable = new DataTable();
                adapter.Fill(patientsTable);
                

                ObservableCollection<Patient> patients = new ObservableCollection<Patient>();

                foreach (DataRow row in patientsTable.Rows)
                {
                    // Convert strings to the given enum type
                    string dbSex = (string)row["Sex"];
                    sex Sex;


                    if (Enum.TryParse(dbSex, out sex parsedSex))
                    {
                        Sex = parsedSex;
                    }
                    else
                    {
                        throw new ArgumentException($"Érvénytelen nem: {dbSex}");
                    }


                    string dbPatientType = (string)row["Type"];
                    patientType Type;


                    if (Enum.TryParse(dbPatientType, out patientType parsedType))
                    {
                        Type = parsedType;
                    }
                    else
                    {
                        throw new ArgumentException($"Érvénytelen Type: {dbPatientType}");
                    }

                    patients.Add(new Patient()
                    {
                        Name = (string)row["Name"],
                        BirthDate = (string)row["BirthDate"],
                        Age = (int)row["Age"],
                        IDnumb = (string)row["IDnumb"],
                        Address = (string)row["Address"],
                        Sex = Sex,
                        HealthCareNumber = (string)row["TAJ"],
                        Type = Type,
                        Room = row["Room"] != DBNull.Value ? (int?)row["Room"] : null
                    });
                }
                return patients;
            }
        }

        //--------------------------------------------------------- Add Patient Data
        public void AddPatientData(Patient patient)
        {
            string query = "INSERT INTO Patients (Name, BirthDate, Age, IDnumb, Address, Sex, TAJ, Type, Room) VALUES (@Name, @BirthDate, @Age, @IDnumb, @Address, @Sex, @TAJ, @Type, @Room)";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Name", patient.Name);
                command.Parameters.AddWithValue("@BirthDate", patient.BirthDate);
                command.Parameters.AddWithValue("@Age", patient.Age);
                command.Parameters.AddWithValue("@IDnumb", patient.IDnumb);
                command.Parameters.AddWithValue("@Address", patient.Address);
                command.Parameters.AddWithValue("@Sex", patient.Sex);
                command.Parameters.AddWithValue("@TAJ", patient.HealthCareNumber);
                command.Parameters.AddWithValue("@Type", patient.Type);
                command.Parameters.AddWithValue("@Room", patient.Room == null ? (object)DBNull.Value : patient.Room);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        //--------------------------------------------------------- Remove Patient Data
        public void RemovePatientData(string idnumb)
        {
            string query = "DELETE FROM Patients WHERE IDnumb=@idnumber";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idnumber", idnumb);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                ManagePeople.patientRemoved = true;
            }
        }

        //--------------------------------------------------------- Clear Patients Data
        public void ClearPatientsData()
        {
            string query = "DELETE FROM Patients";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close() ;
            }
        }



    }
}
