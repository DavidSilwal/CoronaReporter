using System;

namespace CoronaReporter.Data
{
    public class Consultation
    {
        // TODO: Hospital consultation ID?
        public int Id { get; set; }
        
        public Patient Patient { get; set; }
        
        public string PatientId { get; set; }
        
        public DateTime AdmissionTime { get; set; }

        /// <summary>
        /// <c>true</c> is tested positive, <c>false</c> is negative and <c>null</c> means a lab report hasn't
        /// been received yet. 
        /// </summary>
        public bool? LabTestResult { get; set; }
        
        /// <summary>
        /// Whether the patient has been reported to the health authorities.
        /// </summary>
        public bool IsReported { get; set; }

    }
}