namespace VaccOps
{
    using Models;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VaccDb : IVaccOps
    {
        private Dictionary<string, Doctor> doctors = new Dictionary<string, Doctor>();
        private Dictionary<string, Patient> patients = new Dictionary<string, Patient>();

        public void AddDoctor(Doctor doctor)
        {
            if (Exist(doctor))
            {
                throw new ArgumentException();
            }

            doctors.Add(doctor.Name, doctor);
        }

        public void AddPatient(Doctor doctor, Patient patient)
        {
            if (!Exist(doctor))
            {
                throw new ArgumentException();
            }

            patients.Add(patient.Name, patient);
            doctors[doctor.Name].Patients.Add(patient);
            patients[patient.Name].Doctor = doctor;
        }

        public void ChangeDoctor(Doctor oldDoctor, Doctor newDoctor, Patient patient)
        {
            if(!Exist(oldDoctor) || !Exist(newDoctor) || !Exist(patient))
            {
                throw new ArgumentException();
            }

            doctors[oldDoctor.Name].Patients.Remove(patient);

            doctors[newDoctor.Name].Patients.Add(patient);

            patient.Doctor = newDoctor;
        }

        public bool Exist(Doctor doctor) => doctors.ContainsKey(doctor.Name);

        public bool Exist(Patient patient) => patients.ContainsKey(patient.Name);

        public IEnumerable<Doctor> GetDoctors() => doctors.Values.ToArray();

        public IEnumerable<Doctor> GetDoctorsByPopularity(int populariry)
        => doctors.Values.Where(x => x.Popularity == populariry).ToArray();

        public IEnumerable<Doctor> GetDoctorsSortedByPatientsCountDescAndNameAsc()
        => doctors.Values.OrderByDescending(x => x.Patients.Count).ThenBy(x => x.Name);

        public IEnumerable<Patient> GetPatients() => patients.Values.ToArray();

        public IEnumerable<Patient> GetPatientsByTown(string town)
        => patients.Values.Where(x => x.Town == town).ToArray();

        public IEnumerable<Patient> GetPatientsInAgeRange(int lo, int hi)
        => patients.Values.Where(x => x.Age >= lo && x.Age <= hi).ToArray();

        public IEnumerable<Patient> GetPatientsSortedByDoctorsPopularityAscThenByHeightDescThenByAge()
        => patients.Values.OrderBy(x => x.Doctor.Popularity).ThenByDescending(x => x.Height).ThenBy(x => x.Age);

        public Doctor RemoveDoctor(string name)
        {
            if (!doctors.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            var currentDoctor = doctors[name];

            var currentPatients = patients.Values.Where(x => x.Doctor.Name.Equals(name));

            foreach (var patient in currentPatients)
            {
                patients.Remove(patient.Name);
            }

            doctors.Remove(name);


            return currentDoctor;
        }
    }
}
