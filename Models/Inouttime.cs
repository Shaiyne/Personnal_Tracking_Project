using System.ComponentModel.DataAnnotations;

namespace PersonnalTrackingProject.Models
{
    public class Inouttime
    {
        [Key]
        public int InOutTimeID { get; set; }
        [Required]
        public DateTime EntryTime { get; set; }
        public DateTime OutTime { get; set; }

        public int PersonnelID { get; set; }
        public virtual Personnel Personnel { get; set; }


    }
}
