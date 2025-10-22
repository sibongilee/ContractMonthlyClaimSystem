using System;

namespace ContractMonthlyClaimSystem.Models
{
    public class Lecturer
    {
       
        // primary key for each user
        public int Id { get; set; }
        public string LecturerName { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public  DateTime Date {get; set;} = DateTime.Now;
        public string Description { get; set; } = "";

    }//end of class Lecturer
}//end of namespace
