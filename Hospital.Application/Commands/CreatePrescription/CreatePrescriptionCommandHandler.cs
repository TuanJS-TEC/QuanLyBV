using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using MediatR;

namespace Hospital.Application.Commands.CreatePrescription
{
    public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, int>
    {
        private readonly IPrescriptionRepository _repository;

        public CreatePrescriptionCommandHandler(IPrescriptionRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var prescription = new Prescription
            {
                RecordID = request.RecordID,
                DoctorID = request.DoctorID,
                PrescriptionDate = DateTime.UtcNow,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsActive = true
            };

            if (request.Items != null)
            {
                foreach (var item in request.Items)
                {
                    prescription.PrescriptionDetails.Add(new PrescriptionDetail
                    {
                        MedicationID = item.MedicationID,
                        Quantity = item.Quantity,
                        Dosage = item.Dosage,
                        Frequency = item.Frequency,
                        Duration = item.Duration,
                        Instructions = item.Instructions
                    });
                }
            }

            var created = await _repository.AddAsync(prescription);
            return created.PrescriptionID;
        }
    }
}