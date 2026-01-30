using System.Threading.Tasks;
using Hospital.Domain.Entities;

namespace Hospital.Domain.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<MedicalRecord> AddAsync(MedicalRecord record);
    }
}