using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public class User
    {
    [Required]
        // unique identifier for the user
        public int Id { get; set; }
        // FullName of the user
        public string Name { get; set; } = string.Empty;
        //Surname of the user
        public string Surname { get; set; } = string.Empty;
        // Age of the user
        public string Age { get; set; } = string.Empty;
        // Email of the user
        public string Email { get; set; } = string.Empty;
        public string Password{ get; set; } = string.Empty;
        //Role of the user "Lecturer","Programme Coordinator","Academic Manager"
        public string Role{ get; set; } = string.Empty;

    }//end of class User
}//end of namespace
