using ConsultasMedicas.Data;
using ConsultasMedicas.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ConsultasMedicas.Services
{
    public class NotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendNotifications()
        {
            var notifications = await _context.Notifications
                .Where(n => !n.IsSent && n.NotificationDate <= DateTime.Now)
                .Include(n => n.Appointment)
                .ThenInclude(a => a.Patient)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                // Simulação de envio de e-mail
                EmailHelper.SendEmail(notification.Appointment.Patient.Email, "Clinica: Lembre de consulta", $"Você tem uma consulta em breve no dia {notification.Appointment.Date}");
                notification.IsSent = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
