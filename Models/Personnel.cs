using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonnalTrackingProject.Models
{
    public class Personnel
    {
        [Key]
        public int PersonnelID { get; set; }
        
        [Required(ErrorMessage ="Enter a Name.")]
        public string PersonnelName { get; set; }

        [Required]
        public string PersonnelLastName { get; set; }
        
        [DisplayName("Personnel Age")]
        [Range(0,120,ErrorMessage ="Personal age must be between 0 and 120 only !")]
        //[DataType(DataType.Currency)]
        //[Column(TypeName ="byte")]
        public byte PersonnelAge { get; set; }

        public int DepartmentID { get; set; }
        
        public virtual Department Department { get; set; }
        
        public List<Inouttime> Inouttimes { get; set; }
    }
}
