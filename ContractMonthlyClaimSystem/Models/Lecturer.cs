using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public class Lecturer
    {
    [Required]
        // primary key for each user
        public int Id { get; set; }
        public string LecturerName { get; set; } = string.Empty;
        public string Activity { get; set; } = string.Empty;
        public System.DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
    }//end of class Lecturer
}//end of namespace
