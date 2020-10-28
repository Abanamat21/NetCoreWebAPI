using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HRLib.Models.Domain.Abstracts;
using HRLib.Constants;

namespace HRLib.Models.Domain.TSQL
{
    public class TSQLCandidatesReport : ACandidatesReport
    {
        public override void fillReport(DateTime start, DateTime end)
        {     
            Rows = new List<ACandidatesReportRow>();

            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.CandidatesReport, conn);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                conn.Open();

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Rows.Add(new TSQLCandidatesReportRow()
                    {
                        candidateId = dr.GetInt32("candidateid"),
                        candidateName = dr.IsDBNull("candidate") ? "" : dr.GetString("candidate"),
                        candidatePosision = dr.IsDBNull("position") ? "" : dr.GetString("position"),
                        lastTaskRating = dr.IsDBNull("rating") ? "" : dr.GetDouble("rating").ToString()
                    });
                }
                conn.Close();
            }
        
        }
    }
}
