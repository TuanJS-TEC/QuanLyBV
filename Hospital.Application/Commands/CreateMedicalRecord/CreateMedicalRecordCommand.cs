using MediatR;
using System;

namespace Hospital.Application.Commands.CreateMedicalRecord
{
    public class CreateMedicalRecordCommand : IRequest<int>
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int? AppointmentID { get; set; }

        public string? ChiefComplaint { get; set; }
        public string? Symptoms { get; set; }
        public string? Examination { get; set; }

        public decimal? Temperature { get; set; }
        public int? BloodPressureSystolic { get; set; }
        public int? BloodPressureDiastolic { get; set; }
        public int? HeartRate { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        
        public string? Notes { get; set; }
    }
}