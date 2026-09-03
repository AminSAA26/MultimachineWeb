namespace MultimachineWeb.Classes
{
    public class ApplicationSettings
    {
        public string Logpath { get; set; } = "";
        public string TimeInterval { get; set; } = "55000";
        public string diskThreshold { get; set; }
        public string memoryThreshold { get; set; }
        public string machinememorypercent { get; set; }
        public string cpuThreshold { get; set; }
        public string networkThreshold { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string servername { get; set; }
    }
    public class ConnectionStrings
    {
        public string source { get; set; } = string.Empty;
        public string destination { get; set; } = string.Empty;
    }
}
