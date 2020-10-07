using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApi_2_0.Models;

namespace TaskCrowler
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach(int id in GetOverdueTasks())
            {
                setTaskOverdue(id);
                CandidateTask candidateTask = new CandidateTask(id);
                EMailWorcker.sendEMail("Просрочена задача соискателя", $"Задача соискателя {candidateTask.Candidate.Name} номер {candidateTask.ID} просрочена.", new List<string>() { candidateTask.Candidate.HRMail });
            }
        }

        public static List<int> GetOverdueTasks()
        {
            List<int> ret = new List<int>();

            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "SELECT id FROM [dbo].[GetOverdueTasks] ()";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ret.Add(dr.GetInt32(0));
                }
                conn.Close();
            }
            return ret;            
        }

        public static void setTaskOverdue(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
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
