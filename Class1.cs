using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2._1
{
    public class Doctor
    {
        public string LastName { get; set; }
        public string Specialization { get; set; }

    }

    public class HospitalRecord
    {
        public string PatientLastName { get; set; }
        public string DoctorLastName { get; set; }
        public string Diagnosis { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }

    }
}

