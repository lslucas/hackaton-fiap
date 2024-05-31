namespace ConsultasMedicas.Services
{
    using System.Threading.Tasks;
    using ConsultasMedicas.Data;
    using ConsultasMedicas.Models;
    using ConsultasMedicas.Models;
    using Microsoft.EntityFrameworkCore;

    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;

        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> GetPatientAsync(string email)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<bool> UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            return await _context.SaveChangesAsync() > 0;
        }
    }

    public interface IPatientService
    {
        Task<Patient> GetPatientAsync(string email);
        Task<bool> UpdatePatientAsync(Patient patient);
    }
}
