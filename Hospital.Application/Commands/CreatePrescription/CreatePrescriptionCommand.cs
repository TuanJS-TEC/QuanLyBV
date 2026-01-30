using MediatR;
using System.Collections.Generic;

namespace Hospital.Application.Commands.CreatePrescription
{
    public class PrescriptionItemDto
    {
        public int MedicationID { get; set; }
        public int Quantity { get; set; }
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public int Duration { get; set; }
        public string? Instructions { get; set; }
    }

    public class CreatePrescriptionCommand : IRequest<int>
    {
        public int RecordID { get; set; }
        public int DoctorID { get; set; }
        public string? Notes { get; set; }

        public List<PrescriptionItemDto> Items { get; set; } = new();
    }
}