using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CoronaReporter.Model;

namespace CoronaReporter.Data
{
    public class CoronaReporterContext : IdentityDbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        
        public CoronaReporterContext(DbContextOptions<CoronaReporterContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var consultation1 = new Consultation
            {
                Id = 1000,
                PatientId = "1000",
                LabTestResult = true
            };

            var consultation2 = new Consultation
            {
                Id = 1001,
                PatientId = "1001"
            };

            builder.Entity<Consultation>().HasData(consultation1, consultation2);
            
            builder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = "1000",
                    Name = "Shay Rojansky",
                    DateOfBirth = new DateTime(1979, 5, 1),
                },
                new Patient
                {
                    Id = "1001",
                    Name = "Brar Piening",
                    DateOfBirth = new DateTime(1979, 5, 1),
                    IsHighRisk = true
                });
        }
    }
}
