namespace MultimachineWeb.Classes
{
    public class ClsMultiMachineInfo
    {
        public int jsonRecordID { get; set; }
        public string? Platform { get; set; }
        public string? Env { get; set; }
        public string? Location_City_Country { get; set; }
        public string? Server_Name { get; set; }
        public string? Link_IPAddress { get; set; }
        public string? ApplicationID { get; set; }
        public string? Application { get; set; }
        public DateTime? Last_Backup_Date { get; set; }
        public string? Backup_Location { get; set; }
        public string? Backup_Comparison { get; set; }
        public string? Storage_Utilization { get; set; }
        public string? Available_Storage { get; set; }
        public string? Total_Capacity { get; set; }
        public string? Running_Status { get; set; }
        public string? Issue_Description { get; set; }
        public DateTime? Issue_Reported_Date { get; set; }
        public DateTime? Issue_Resolution_Date { get; set; }
        public string? Duration { get; set; }
        public string? Resolution_Status { get; set; }
        public string? Responsible_Person { get; set; }
        public string? Next_Payment { get; set; }
        public string? Updates { get; set; }
        public DateTime? EnteredDate { get; set; }
        public List<ClsIssueData> IssuesList { get; set; }=new List<ClsIssueData>();
    }
}
