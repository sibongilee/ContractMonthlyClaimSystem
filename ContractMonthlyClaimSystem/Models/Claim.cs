using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int claimId { get; set; }
        public string claimType { get; set; } = string.Empty;
        public string claimDescription { get; set; } = string.Empty;
        [Required]
        public string LecturerName { get; set; } = string.Empty;//name of the lecturer who made the claim
        [Required]
        public double HoursWorked { get; set; }
        [Required]
        public double HourlyRate { get; set; }
        public string Notes { get; set; }//notes from the verifier

        public string Status { get; set; } = "Pending";//status of the claim: Pending, Approved, Rejected
        public string DocumentPath { get; set; } 
        public System.DateTime claimDate { get; set; }
        public double TotalAmount => HoursWorked * HourlyRate;
       
    }//end of class Claim
}//end of namespace
