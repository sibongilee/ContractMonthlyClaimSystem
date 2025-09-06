namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int claimId { get; set; }
        public string claimType { get; set; } = string.Empty;
        public string claimDescription { get; set; } = string.Empty;
        public System.DateTime claimDate { get; set; }
        public string Status { get; set; } = string.Empty;//Pending, Approved, Rejected
        public string Documents { get; set; } = string.Empty;//file path of the document
        // link to User
        public int UserId { get; set; }
        public string SubmittedBy { get; set; } = string.Empty;//name of the user who submitted the claim
        // link to Lecturer
        public int LecturerId { get; set; }
        public string LecturerName { get; set; } = string.Empty;//name of the lecturer who made the claim
    }//end of class Claim
}//end of namespace
