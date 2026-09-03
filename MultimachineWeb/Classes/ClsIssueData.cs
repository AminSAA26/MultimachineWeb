namespace MultimachineWeb.Classes
{
    public class ClsIssueData
    {
        public int         RecordID { get; set; }
        public string? ApplicationID { get; set; }
        public string? Application { get; set; }
        public string  Issue_Description { get; set; }
        public DateOnly ?   Issue_Reported_Date { get; set; }
        public TimeOnly Issue_Reported_Time { get; set; }
        
        public DateOnly? Issue_Resolution_Date { get; set; }
        public TimeOnly? Issue_Resolution_Time { get; set; }

        public string? Duration {  get; set; }
        public string? Resolution_Status { get; set; }
        public int? multimachineinfo_RecordID { get; set; }
        public DateTime EntryDate { get; set; }

    }
}
