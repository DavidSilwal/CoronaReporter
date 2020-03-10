using System;
using CoronaReporter.Data;

namespace CoronaReporter.Service
{
    public interface IMedicalDataSource : IDisposable
    {
        public event GenericEventHandler<AdmissionArgs> PatientAdmitted;
        //public event GenericEventHandler<ConsultationArgs> ConsultationCompleted;
        public event GenericEventHandler<LabReportArgs> LabReportReceived;
    }

    public abstract class MedicalDataArgsBase
    {
        public string PatientId { get; set;  }
    }

    public class AdmissionArgs : MedicalDataArgsBase
    {
        public DateTime AdmissionTime { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
    
    // public class ConsultationArgs : MedicalDataArgsBase
    // {
    //     public ConsultationArgs(string patientId) : base(patientId) {}
    // }

    public class LabReportArgs : MedicalDataArgsBase
    {
        public bool TestResult { get; set; }
    }
}