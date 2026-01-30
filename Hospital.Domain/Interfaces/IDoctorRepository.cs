using Hospital.Domain.Entities;

namespace Hospital.Domain.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor> AddAsync(Doctor doctor);
    }
};

