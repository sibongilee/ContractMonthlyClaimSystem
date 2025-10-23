using System;

namespace ContractMonthlyClaimSystem.Models
{
    public class Lecturer
    {
       
        // primary key for each user
        public int LecturerId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string LecturerName { get; set; }
        public string Topic { get; set; } = string.Empty;
        public  DateTime Date {get; set;} = DateTime.Now;
        public string Department{ get; set; } = "";
        public int UserId { get; set; }//foreign key referencing User table

    }//end of class Lecturer
}//end of namespace
