using System.Threading.Tasks;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Interfaces
{
    public interface IPrescriptionRepository
    {
        Task<Prescription> AddAsync(Prescription prescription);
    }
}