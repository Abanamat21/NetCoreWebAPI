using System;
using System.Data;
using System.Data.SqlClient;

namespace WebApi_2_0.Models
{
    public class CandidateTask
    {
        /// <summary>
        /// id, NOT NULL
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// candidate_id, NOT NULL
        /// </summary>
        public Candidate Candidate { get; set; }
        /// <summary>
        /// descr, NULL
        /// </summary>
        public String Descr { get; set; }
        /// <summary>
        /// inspector, NULL
        /// </summary>
        public String InspectorName { get; set; }
        /// <summary>
        /// receipt_dtime, NOT NULL
        /// </summary>
        public DateTime ReceiptDate { get; set; }
        /// <summary>
        /// expected_completion_dtime, NOT NULL
        /// </summary>
        public DateTime ExpectedCompletionDate { get; set; }
        /// <summary>
        /// completion_dtime, NULL
        /// </summary>
        public DateTime FactCompletionDate { get; set; }
        /// <summary>
        /// status_id, NOT NULL
        /// </summary>
        public CandidateTaskState State { get; set; }
        /// <summary>
        /// inspector_rating, NULL
        /// </summary>
        public double InspectorRating { get; set; }

        public CandidateTask()
        {
        }
        public CandidateTask(int id)
        {
            dbSelect(id);
        }

        /// <summary>
        /// Получить задачу из БД
        /// </summary>   
        private void dbSelect(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "SELECT top 1 * FROM [dbo].[SelectCandidateTask] (@id)";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ID = dr.GetInt32("id");
                    Candidate = new Candidate(dr.GetInt32("candidate_id"));
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
        public void dbInsert()
        {
            if (ExpectedCompletionDate == DateTime.MinValue) throw new ArgumentException("ExpectedCompletionDate is not seted");
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[InsertCandidateTask] @candidate_id, @descr, @dtime, @expected_completion_dtime";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
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
        public void dbUpdateCompleted(DateTime completionDTime)
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[UpdateCandidateTaskCompleted] @id, @completiondtime";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
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
        public void dbUpdateChecked(String inspector, float rating)
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[UpdateCandidateTaskChecked] @id, @inspector, @rating";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
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
        public void dbDelete()
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[DeleteCandidateTask] @id";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    public enum CandidateTaskState
    {
        Sended = 1,
        Finished = 2,
        Checked = 3
    }


}
