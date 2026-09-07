using Microsoft.Data.SqlClient;
using MultimachineWeb.Classes;
using System;
using System.Data;
using System.Text.Json;

namespace MultimachineWeb.Classes
{

    public class ClsUpdate
    {
        ApplicationSettings _applicationSettings;
        ConnectionStrings _connectionStrings;
        List<ClsMultiMachineInfo> _infolist = new List<ClsMultiMachineInfo>();
        public ClsUpdate(ApplicationSettings applicationSettings,ConnectionStrings connectionStrings) 
        { 
        _applicationSettings = applicationSettings; 
            _connectionStrings = connectionStrings;
        }


        public bool updateData(ClsMultiMachineInfo _clsmulti)
        {
            bool result = false;
            string sql = @"update automate.multimachineinfo set Issue_Description=@des,Issue_Reported_Date=@issuedate,Issue_Resolution_Date=@resoludate,
                        Duration=@dur,Resolution_Status=@status where RecordID=@id";
            try {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = _connectionStrings.source;
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = sql;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new SqlParameter("@des", SqlDbType.NVarChar)).Value = _clsmulti.Issue_Description;
                        cmd.Parameters.Add(new SqlParameter("@issuedate", SqlDbType.DateTime)).Value = _clsmulti.Issue_Reported_Date;
                        cmd.Parameters.Add(new SqlParameter("@resoludate", SqlDbType.DateTime)).Value = _clsmulti.Issue_Resolution_Date;
                        cmd.Parameters.Add(new SqlParameter("@dur", SqlDbType.NVarChar)).Value = _clsmulti.Duration;
                        cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.NVarChar)).Value = _clsmulti.Resolution_Status;
                        cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = _clsmulti.jsonRecordID;
                        ;

                        var _d = cmd.ExecuteScalar();

                        cmd.Dispose();

                    }
                    conn.Close();
                }
            } catch (Exception ex) 
            { 
            
            
            }
            return result;
        }


        public string GetList(string _servername,string _datetime)
        {
            List<ClsMultiMachineInfo> _list = new List<ClsMultiMachineInfo>();

            try
            {
                _infolist.Clear();
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = _connectionStrings.source;
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        //string _sql = "select * from multimachineinfo where cast(EnteredDate as date) ='2026-08-17'";
                        //string _sql = $"select * from automate.multimachineinfo where cast(EnteredDate as date) ='{_datetime}'";
                        string _sql = $"select * from automate.multimachineinfo where cast(EnteredDate as date) ='{_datetime}' and Server_Name='{_servername}'";
                        cmd.CommandText = _sql;
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ClsMultiMachineInfo _info = new ClsMultiMachineInfo();
                            _info.jsonRecordID = Convert.ToInt32(reader["RecordID"].ToString());
                            _info.Platform = reader["Platform"].ToString();
                            _info.Env = reader["Env"].ToString();
                            _info.Location_City_Country = reader["Location_City_Country"].ToString();
                            _info.Server_Name = reader["Server_Name"].ToString();
                            _info.Link_IPAddress = reader["Link_IPAddress"].ToString();
                            _info.ApplicationID = reader["ApplicationID"].ToString();
                            _info.Application = reader["Application"].ToString();
                            _info.Last_Backup_Date = (reader["Last_Backup_Date"] != DBNull.Value)? Convert.ToDateTime( reader["Last_Backup_Date"].ToString()):null;
                            _info.Backup_Location = reader["Backup_Location"].ToString();
                            _info.Backup_Comparison = reader["Backup_Comparison"].ToString();
                            _info.Storage_Utilization = reader["Storage_Utilization"].ToString();
                            _info.Available_Storage = reader["Available_Storage"].ToString();
                            _info.Total_Capacity = reader["Total_Capacity"].ToString();
                            _info.Running_Status = reader["Running_Status"].ToString();
                            _info.Issue_Description = reader["Issue_Description"].ToString();
                            _info.Issue_Reported_Date = (reader["Issue_Reported_Date"] != DBNull.Value) ? Convert.ToDateTime(reader["Issue_Reported_Date"].ToString()) : null;
                            _info.Issue_Resolution_Date = (reader["Issue_Resolution_Date"] != DBNull.Value) ? Convert.ToDateTime(reader["Issue_Resolution_Date"].ToString()) : null;
                            _info.Duration = reader["Duration"].ToString();
                            _info.Resolution_Status = reader["Resolution_Status"].ToString();
                            _info.Responsible_Person = reader["Responsible_Person"].ToString();
                            _info.Next_Payment = reader["Next_Payment"].ToString();
                            _info.Updates = reader["Updates"].ToString();
                            _info.EnteredDate = Convert.ToDateTime(reader["EnteredDate"].ToString());
                            
                            _infolist.Add(_info);
                        }
                        reader.Close();
                        cmd.Dispose();
                    }
                    ////////////////////////////////////
                    ;
                    var _inlist  = _infolist.Select(x => x.jsonRecordID).ToList();
                    using(SqlCommand cmd =  new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = $" select * from automate.issues_data where multimachineinfo_RecordID in ({string.Join(",", _inlist)})";
                        SqlDataReader _rd = cmd.ExecuteReader();
                        while (_rd.Read())
                        {
                            ClsIssueData _iss = new ClsIssueData();
                            _iss.RecordID = Convert.ToInt32(_rd["RecordID"].ToString());
                            _iss.ApplicationID = _rd["ApplicationID"].ToString();
                            _iss.Application = _rd["ApplicationName"].ToString();
                            _iss.Issue_Description = _rd["Issue_Description"].ToString();
                            _iss.Duration = _rd["Duration"].ToString() ;
                            _iss.Resolution_Status = _rd["Resolution_Status"].ToString();
                            //_iss.Issue_Reported_Date = Convert.ToDateTime( _rd["Issue_Reported_Date"].ToString());
                            var _d = Convert.ToDateTime(_rd["Issue_Reported_Date"].ToString());
                            _iss.Issue_Reported_Date = DateOnly.FromDateTime(_d);
                            _iss.Issue_Reported_Time = TimeOnly.FromDateTime(_d);
                            ;
                            DateTime? _r =(_rd["Issue_Resolution_Date"]!=DBNull.Value) ? Convert.ToDateTime(_rd["Issue_Resolution_Date"].ToString()):null;
                            if (_r!=null)
                            {
                                _iss.Issue_Resolution_Date = DateOnly.FromDateTime(_r.Value);
                                _iss.Issue_Resolution_Time = TimeOnly.FromDateTime(_r.Value);
                            }

                            //_iss.Issue_Reported_Date = Convert.ToDateTime(_rd["Issue_Reported_Date"].ToString());
                            _iss.multimachineinfo_RecordID = Convert.ToInt32(_rd["multimachineinfo_RecordID"].ToString()) ;
                            _iss.EntryDate = Convert.ToDateTime(_rd["EntryDate"].ToString());
                            var _ll = _infolist.Where(m => m.jsonRecordID == _iss.multimachineinfo_RecordID).FirstOrDefault();
                            ;
                            _ll.IssuesList.Add(_iss);
                        }
                        _rd.Close();
                        cmd.Dispose();
                    }
                    conn.Close();
                }

                

                ;
                var _json = GetMachineInfoAsJson(_infolist);
                return _json;
            }
            catch (Exception e)
            {


            }

            return ""; ;
        }
        public string GetMachineInfoAsJson(List<ClsMultiMachineInfo> infolist)
         {
            // Options to make the output look clean and readable
            var options = new JsonSerializerOptions { WriteIndented = true };

            // Serializes the object into a raw JSON string
            return JsonSerializer.Serialize(infolist, options);
        }
    }
}
