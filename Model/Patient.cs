using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalRecord.Model
{
    public enum patientType
    {
        Járó,
        Fekvő
    }

    public class Patient : People
    {

        public string HealthCareNumber { get; set; }
        public int? Room { get; set; }
        public patientType Type { get; set; }

        public Patient()
        {
        }
    }
}
