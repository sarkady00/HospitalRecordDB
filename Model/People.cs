using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRecord.Model
{
    public enum sex
    {
        Férfi,
        Nő
    }

    public abstract class People
    {
        private static DateTime nowDate = DateTime.Today;

        public string Name { get; set; }
        public string IDnumb { get; set; }
        public string Address { get; set; }
        public sex Sex { get; set; }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private string birthDate;
        public string BirthDate
        {
            get { return birthDate; }
            set
            {
                // Calculate the age of a person according to their birthdate
                var age_temp = DateTime.Today.Year - Convert.ToDateTime(value).Year;
                if (Convert.ToDateTime(value).Date > DateTime.Today.AddYears(-age_temp)) age_temp--;
                Age = age_temp;
                birthDate = value;
            }
        }

        public People()
        {

        }
    }
}
