using System.ComponentModel.DataAnnotations;

namespace PersonnalTrackingProject.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }

        public string UserLastName { get; set; }

        public string Email { get; set; }

        public string UserPassword { get; set; }



    }
}
