using System;
using System.Threading;
using System.Threading.Tasks;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using MediatR;

namespace Hospital.Application.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, int>
    {
        private readonly IDoctorRepository _doctorRepository;

        public CreateDoctorCommandHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<int> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Specialization = request.Specialization,
                LicenseNumber = request.LicenseNumber,
                Phone = request.Phone,
                Email = request.Email,
                DepartmentID = request.DepartmentID,
                ConsultationFee = request.ConsultationFee,
                ExperienceYears = request.ExperienceYears,
                Qualification = request.Qualification,
                
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
                IsAvailable = true
                // IsActive = true
            };

            var createdDoctor = await _doctorRepository.AddAsync(doctor);
            return createdDoctor.DoctorID;
        }
    }
}