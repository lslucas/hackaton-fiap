namespace ConsultasMedicas.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Message { get; set; }
        public bool IsSent { get; set; }
        public Appointment Appointment { get; set; }
    }
}
