using System;

namespace CoronaReporter.Model
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
        /// Whether an anamnesis has been properly populated for this patient consultation, whether automatic
        /// or manual.
        /// </summary>
        public bool IsAnamnesisPopulated { get; set; }
        
        /// <summary>
        /// Whether the admission has been reported to the health authorities.
        /// </summary>
        public bool IsAdmissionReported { get; set; }
        
        /// <summary>
        /// Whether the test result has been reported to the health authorities.
        /// </summary>
        public bool IsTestResultReported { get; set; }
    }
}