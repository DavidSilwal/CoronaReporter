using System;
using System.Collections.Generic;
using CoronaReporter.Service;

namespace CoronaReporter.Model
{
    public class Patient
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Whether the patient is considered high-risk for spreading the disease, e.g. teachers, health works.
        /// </summary>
        public bool IsHighRisk { get; set; }

        public List<Consultation> Consultations { get; set; } = new List<Consultation>();

        internal void PopulateFrom(AdmissionArgs admissionArgs)
        {
            Id = admissionArgs.PatientId;
            Name = admissionArgs.Name;
            DateOfBirth = admissionArgs.DateOfBirth;
        }
        
        public override string ToString()
            => $"Id={Id} Name={Name}";
    }
}