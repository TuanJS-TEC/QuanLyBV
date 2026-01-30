using System.Threading.Tasks;
using Hospital.Domain.Entities;
using Hospital.Domain.Interfaces;
using Hospital.Infrastructure.Data;

namespace Hospital.Infrastructure.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly HospitalDbContext _context;

        public PrescriptionRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<Prescription> AddAsync(Prescription prescription)
        {
            await _context.Prescriptions.AddAsync(prescription);
            await _context.SaveChangesAsync();
            return prescription;
        }
    }
}