using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2._1
{
    internal class Program
    {
        //14) Елементи колекції «Лікарі» мають наступну структуру: прізвище лікаря,
        // спеціалізація. Елементи колекції «Лікарня» містять прізвище пацієнта, 
        // прізвище лікаря, діагноз, дата надходження, дата виписки.
        //a) Вивести прізвища лікарів, які ставили діагнози заданому пацієнту у поточному році.
        //b) Вивести список прізвищ пацієнтів та сумарну тривалість їх перебування у лікарні.
        //c) Вивести спеціалізації лікарів, лікарі яких загалом поставили найбільшу кількість різних діагнозів.



        static void Main(string[] args)
        {
            //Задання поточних даних

            List<Doctor> doctors = new List<Doctor>
{
    new Doctor {LastName = "House", Specialization ="Neurologist"},
    new Doctor {LastName = "Jones", Specialization ="Therapist"},
    new Doctor {LastName = "Smith", Specialization ="Cardiologist"},
    new Doctor {LastName = "Brown", Specialization ="Pediatrician"},

};

            List<HospitalRecord> patients = new List<HospitalRecord>
            {
    new HospitalRecord { PatientLastName = "Williams", DoctorLastName = "House", Diagnosis = "Heart Attack"  , AdmissionDate = new DateTime(2023,11,12),DischargeDate = new DateTime(2024,4,25)},
    new HospitalRecord { PatientLastName = "Davis", DoctorLastName = "Brown", Diagnosis = "Migraine"  , AdmissionDate = new DateTime(2024,1,22),DischargeDate = new DateTime(2024,2,2)},
    new HospitalRecord { PatientLastName = "Garcia", DoctorLastName = "Smith", Diagnosis =  "Appendectomy" , AdmissionDate = new DateTime(2024,6,13),DischargeDate = new DateTime(2024,7,1)},
    new HospitalRecord { PatientLastName = "Miller", DoctorLastName = "House", Diagnosis =  "Bronchitis" , AdmissionDate = new DateTime(2022,12,21),DischargeDate = new DateTime(2022,12,23)},
    new HospitalRecord { PatientLastName = "Scott", DoctorLastName = "Jones", Diagnosis =  "Heart Attack" , AdmissionDate = new DateTime(2024,3,12),DischargeDate = new DateTime(2024,3,31)},
    new HospitalRecord { PatientLastName = "Mitchell", DoctorLastName = "House", Diagnosis =  "Flu" , AdmissionDate = new DateTime(2023,11,12),DischargeDate = new DateTime(2023,12,25)},
            };

            //a) Вивести прізвища лікарів, які ставили діагнози заданому пацієнту у поточному році.

            string PatientLastName = "Garcia";
            int currentYear = DateTime.Now.Year;

            var doctorsHospital = patients
            .Where(p => p.PatientLastName == PatientLastName && p.AdmissionDate.Year == currentYear)
            .Select(p => p.DoctorLastName)
            .Distinct();

            Console.WriteLine($"Лікарі, які лікували {PatientLastName} в {currentYear}:");
            foreach (var doctor in doctorsHospital)
            {
                Console.WriteLine(doctor);
            }

            //b) Вивести список прізвищ пацієнтів та сумарну тривалість їх перебування у лікарні.

            var patientDurations = patients
            .GroupBy(p => p.PatientLastName)
            .Select(g => new
            {
                PatientLastName = g.Key,
                TotalDays = g.Sum(p => (p.DischargeDate.HasValue ? p.DischargeDate.Value : DateTime.Now) 
                .Subtract(p.AdmissionDate).Days)
            })
            .ToList();

           
            Console.WriteLine("Список всіх пацієнтів та загальна тривалість перебування в лікарні :");
            foreach (var patient in patientDurations)
            {
                Console.WriteLine($"Пацієнт: {patient.PatientLastName}, Загальна кількість днів в лікарні: {patient.TotalDays} days");
            }

            //c) Вивести спеціалізації лікарів, лікарі яких загалом поставили найбільшу кількість різних діагнозів.

            var specializations = patients
            .Join(doctors, p => p.DoctorLastName, d => d.LastName,(p, d) => new { d.Specialization, p.Diagnosis }) 
            .GroupBy(x => x.Specialization) 
            .Select(g => new
            {
                Specialization = g.Key,
                UniqueDiagnosesCount = g.Select(x => x.Diagnosis).Distinct().Count() 
            })
            .OrderByDescending(x => x.UniqueDiagnosesCount) 
            .ToList();

            Console.WriteLine("Спеціалізацій з найбільшою кількістю унікальних діагнозів:");
            foreach (var specialization in specializations)
            {
                Console.WriteLine($"Спеціадізація: {specialization.Specialization}, Унікальний діагноз: {specialization.UniqueDiagnosesCount}");
            }

            Console.ReadLine(); 
        }
    }
}





    








