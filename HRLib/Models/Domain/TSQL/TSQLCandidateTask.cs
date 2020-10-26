using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HRLib.Constants;
using HRLib.Models.Domain.Abstracts;
using HRLib.Models.Domain.Enums;

namespace HRLib.Models.Domain.TSQL
{
    public class TSQLCandidateTask : ACandidateTask
    {
        public TSQLCandidateTask()
        {
        }
        public TSQLCandidateTask(int id)
        {
            dbSelect(id);
        }

        /// <summary>
        /// Получить задачу из БД
        /// </summary>   
        public override void dbSelect(int id)
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.SelectCandidateTask, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ID = dr.GetInt32("id");
                    Candidate = new TSQLCandidate(dr.GetInt32("candidate_id"));
                    Descr = dr.IsDBNull("descr") ? "" : dr.GetString("descr");
                    InspectorName = dr.IsDBNull("inspector") ? "" : dr.GetString("inspector");
                    ReceiptDate = dr.GetDateTime("receipt_dtime");
                    ExpectedCompletionDate = dr.GetDateTime("expected_completion_dtime");
                    FactCompletionDate = dr.IsDBNull("completion_dtime") ? DateTime.MinValue : dr.GetDateTime("completion_dtime");
                    State = (CandidateTaskState)dr.GetInt32("id");
                    InspectorRating = dr.IsDBNull("inspector_rating") ? -1d : dr.GetDouble("inspector_rating");
                }
                conn.Close();
            }
        }
        /// <summary>
        /// Занести новую задачу в БД
        /// </summary> 
        public override void dbInsert()
        {
            if (ExpectedCompletionDate == DateTime.MinValue) throw new ArgumentException("ExpectedCompletionDate is not seted");
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.InsertCandidateTask, conn);
                cmd.Parameters.AddWithValue("@candidate_id", Candidate.ID);
                cmd.Parameters.AddWithValue("@descr", Descr ?? "");
                cmd.Parameters.AddWithValue("@dtime", ReceiptDate == DateTime.MinValue ? DateTime.Now : ReceiptDate);
                cmd.Parameters.AddWithValue("@expected_completion_dtime", ExpectedCompletionDate);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        /// <summary>
        /// Отметить факт выполнения задачи
        /// </summary> 
        public override void dbUpdateCompleted(DateTime completionDTime)
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.UpdateCandidateTaskCompleted, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@completiondtime", completionDTime);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        /// <summary>
        /// Установить задаче оценку
        /// </summary> 
        public override void dbUpdateChecked(String inspector, float rating)
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmdStr = SqlCommands.UpdateCandidateTaskChecked;
                var cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@inspector", inspector);
                cmd.Parameters.AddWithValue("@rating", rating);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        /// <summary>
        /// Удалить задачу из БД
        /// </summary>  
        public override void dbDelete()
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.DeleteCandidateTask, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
