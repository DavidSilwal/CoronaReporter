using System;
using System.Collections.Generic;
using System.Linq;
using CoronaReporter.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoronaReporter.Service
{
    public class MedicalDataManager : IDisposable
    {
        readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;
        readonly IMedicalDataSource _dataSource;
        
        public event GenericEventHandler<Consultation> PatientAdmitted;
        public event GenericEventHandler<Consultation> LabReportReceived;

        public MedicalDataManager(IServiceProvider serviceProvider, ILogger<MedicalDataManager> logger, IMedicalDataSource dataSource)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _dataSource = dataSource;
            _dataSource.PatientAdmitted += OnPatientAdmitted;
            _dataSource.LabReportReceived += OnLabReportedReceived;
        }

        void OnPatientAdmitted(AdmissionArgs admissionArgs)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            using var ctx = serviceScope.ServiceProvider.GetService<CoronaReporterContext>();

            var patient = ctx.Patients
                .Include(p => p.Consultations)
                .SingleOrDefault(p => p.Id == admissionArgs.PatientId);

            if (patient == null)
            {
                patient = new Patient();
                patient.PopulateFrom(admissionArgs);
                ctx.Patients.Add(patient);
                _logger.LogInformation($"New patient admission: {patient}");
            }
            else
            {
                patient.PopulateFrom(admissionArgs);
                _logger.LogInformation($"New admission for existing patient: {patient}");
            }

            // TODO: Two consultations open at the same time? Understand consultation logic
            // (is there an ID?)
            var consultation = new Consultation
            {
                Patient = patient,
                AdmissionTime = admissionArgs.AdmissionTime
            };
            patient.Consultations.Add(consultation);
            
            ctx.SaveChanges();
            
            PatientAdmitted?.Invoke(consultation);
        }

        void OnLabReportedReceived(LabReportArgs labReportArgs)
        {
            using var serviceScope = _serviceProvider.CreateScope();
            using var ctx = serviceScope.ServiceProvider.GetService<CoronaReporterContext>();

            var patient = ctx.Patients
                .Include(p => p.Consultations)
                .SingleOrDefault(p => p.Id == labReportArgs.PatientId);
            
            if (patient == null)
            {
                _logger.LogError($"Received lab report (TestResult={labReportArgs.TestResult}) for unknown patient with ID: {labReportArgs.PatientId}");
                return;
            }

            var consultation = patient.Consultations.SingleOrDefault(c => !c.IsReported);
            if (consultation == null)
            {
                _logger.LogError($"Received lab report (TestResult={labReportArgs.TestResult}), but no open consultation found for patient: {patient}");
                return;
            }
            
            consultation.LabTestResult = labReportArgs.TestResult;
            ctx.SaveChanges();
            
            LabReportReceived?.Invoke(consultation);
            
            _logger.LogInformation($"Lab test result {consultation.LabTestResult} received for patient {patient}");
        }

        public void Dispose()
        {
            _dataSource.PatientAdmitted -= OnPatientAdmitted;
            _dataSource.LabReportReceived -= OnLabReportedReceived;
        }
    }
}