using System;
using System.Threading;
using CoronaReporter.Data;

namespace CoronaReporter.Service
{
    public class DummyMedicalDataSource : IMedicalDataSource
    {
        readonly Timer _timer;
        volatile int _patientId = 1;
        
        public event GenericEventHandler<AdmissionArgs> PatientAdmitted;
        //public event GenericEventHandler<ConsultationArgs> ConsultationCompleted;
        public event GenericEventHandler<LabReportArgs> LabReportReceived;

        public DummyMedicalDataSource(TimeSpan interval)
            //=> _timer = new Timer(OnElapsed, null, interval, TimeSpan.FromMilliseconds(-1));
             => _timer = new Timer(OnElapsed, null, interval, interval);

        void OnElapsed(object state)
        {
            var newPatientId = Interlocked.Increment(ref _patientId);
            
            PatientAdmitted?.Invoke(new AdmissionArgs
            {
                PatientId = newPatientId.ToString(),
                FirstName = "John",
                LastName = "Doe" + newPatientId,
                DateOfBirth = new DateTime(1979, 1, 1)
            });
            
            LabReportReceived?.Invoke(new LabReportArgs
            {
                PatientId = newPatientId.ToString(),
                TestResult = true 
            });
        }
        
        public void Dispose() => _timer.Dispose();
    }
}