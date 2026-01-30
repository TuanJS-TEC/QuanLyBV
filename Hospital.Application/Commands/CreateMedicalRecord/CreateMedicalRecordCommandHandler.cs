using System;
using System.Threading;
using System.Threading.Tasks;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using MediatR;

namespace Hospital.Application.Commands.CreateMedicalRecord
{
    public class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, int>
    {
        private readonly IMedicalRecordRepository _repository;

        public CreateMedicalRecordCommandHandler(IMedicalRecordRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var record = new MedicalRecord
            {
                PatientID = request.PatientID,
                DoctorID = request.DoctorID,
                AppointmentID = request.AppointmentID,
                VisitDate = DateTime.UtcNow,
                
                ChiefComplaint = request.ChiefComplaint,
                Symptoms = request.Symptoms,
                Examination = request.Examination,
                
                Temperature = request.Temperature,
                BloodPressureSystolic = request.BloodPressureSystolic,
                BloodPressureDiastolic = request.BloodPressureDiastolic,
                HeartRate = request.HeartRate,
                Weight = request.Weight,
                Height = request.Height,
                
                Notes = request.Notes,
                
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsActive = true
            };

            var createdRecord = await _repository.AddAsync(record);
            return createdRecord.RecordID;
        }
    }
}