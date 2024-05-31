using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultasMedicas.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public bool IsCancelled { get; set; }
        [NotMapped]
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
