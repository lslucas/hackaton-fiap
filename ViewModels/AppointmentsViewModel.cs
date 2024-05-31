using ConsultasMedicas.Data;
using ConsultasMedicas.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConsultasMedicas.ViewModels
{
    public class AppointmentViewModel
    {
        [Required]
        [Display(Name = "Appointments")]
        public List<Appointment> Appointments { get; set; }

        [Required]
        [Display(Name = "Doctor")]
        public DbSet<Doctor> Doctor { get; set; }
    }

}
