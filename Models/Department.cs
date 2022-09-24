using System.ComponentModel.DataAnnotations;

namespace PersonnalTrackingProject.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        public string DepartmentLocation { get; set; }
        public List<Personnel> Personals { get; set; }
        //public List<Inouttime> Inouttimes { get; set; }
    }
}
