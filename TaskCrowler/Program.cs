using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HRLib.Constants;
using HRLib.Interfaces;
using HRLib.Models.Domain.Abstracts;
using HRLib.Models.Domain.TSQL;
using HRLib.Services;

namespace TaskCrowler
{
    class Program
    {
        static IEMailWorcker eMailWorcker = new EMailWorcker();
        static void Main(string[] args)
        {
            foreach (int id in GetOverdueTasks())
            {
                setTaskOverdue(id);
                ACandidateTask candidateTask = new TSQLCandidateTask(id);
                eMailWorcker.sendEMail("Просрочена задача соискателя", $"Задача соискателя {candidateTask.Candidate.Name} номер {candidateTask.ID} просрочена.", new List<string>() { candidateTask.Candidate.HRMail });
            }
        }

        public static List<int> GetOverdueTasks()
        {
            List<int> ret = new List<int>();

            using (SqlConnection conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                String cmdStr = "SELECT id FROM [dbo].[GetOverdueTasks] ()";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                conn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    ret.Add(Int32.Parse(dr["id"].ToString()));
                }
                conn.Close();
            }
            return ret;            
        }

        public static void setTaskOverdue(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[UpdateCandidateTaskOverdue] @id";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}
