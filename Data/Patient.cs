using System;
using System.Collections.Generic;
using CoronaReporter.Service;

namespace CoronaReporter.Data
{
    public class Patient
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
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
            FirstName = admissionArgs.FirstName;
            LastName = admissionArgs.LastName;
            DateOfBirth = admissionArgs.DateOfBirth;
        }
        
        public override string ToString()
            => $"Id={Id} FirstName={FirstName} LastName={LastName}";
    }
}