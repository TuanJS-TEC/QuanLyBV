using Hospital.Domain.Entities;

namespace Hospital.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> AddAsync(Appointment appointment);
    }
};

