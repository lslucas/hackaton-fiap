using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsultasMedicas.Models
{
    public class Patient : IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        [EmailAddress]
        public override string Email { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        //[ForeignKey("PatientId")]
        //public ICollection<Appointment> Appointments { get; set; }
    }
}
