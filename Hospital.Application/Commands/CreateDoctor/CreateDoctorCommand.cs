using MediatR;
using System;

namespace Hospital.Application.Commands.CreateDoctor
{
    public class CreateDoctorCommand : IRequest<int>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        public int DepartmentID { get; set; } 
        
        public decimal ConsultationFee { get; set; }
        public int ExperienceYears { get; set; }
        public string Qualification { get; set; } = string.Empty;
    }
}