using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRecord.Model
{
    public enum specialty
    {
        Általános,
        Ortopédus,
        Szemész,
        Kardiológus,
        Sebész,
        Urulógus,
        Nőgyógyász
    }

    public enum job
    {
        Ápoló,
        Orvos
    }

    public class Employee : People
    {
        public int? PatientsNumb { get; set; }
        public int? Salary { get; set; }
        public specialty Specialty { get; set; }
        public job Job { get; set; }

        public Employee()
        {
        }
    }
}
