using System.Threading.Tasks;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using Hospital.Infrastructure.Data;

namespace Hospital.Infrastructure.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly HospitalDbContext _context;

        public MedicalRecordRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<MedicalRecord> AddAsync(MedicalRecord record)
        {
            await _context.MedicalRecords.AddAsync(record);
            await _context.SaveChangesAsync();
            return record;
        }
    }
}