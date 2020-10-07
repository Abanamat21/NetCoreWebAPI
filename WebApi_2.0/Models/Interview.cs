using System;
using System.Data;
using System.Data.SqlClient;

namespace WebApi_2_0.Models
{
    public class Interview
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
        /// interviewer, NOT NULL
        /// </summary>
        public String InterviewerName { get; set; }
        /// <summary>
        /// dtime, NOT NULL
        /// </summary>
        public DateTime DTime { get; set; }
        /// <summary>
        /// status_id, NOT NULL
        /// </summary>
        public InterviewState State { get; set; }

        public Interview(int id)
        {
            dbSelect(id);
        }
        public Interview()
        {

        }

        /// <summary>
        /// Получить интерьвью из БД
        /// </summary>   
        private void dbSelect(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "SELECT top 1 * FROM [dbo].[SelectInterview] (@id)";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ID = dr.GetInt32("id");
                    Candidate = new Candidate(dr.GetInt32("candidate_id"));
                    InterviewerName = dr.GetString("interviewer");
                    DTime = dr.GetDateTime("dtime");
                    State = (InterviewState)dr.GetInt32("status_id");
                }
                conn.Close();
            }
        }
        /// <summary>
        /// Занести новое интерьвью в БД
        /// </summary>  
        internal void dbInsert()
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[InsertInterview] @candidate_id, @dtime, @interviewer";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@candidate_id", Candidate.ID);
                cmd.Parameters.AddWithValue("@dtime", DTime == DateTime.MinValue ? DateTime.Now : DTime);
                cmd.Parameters.AddWithValue("@interviewer", InterviewerName ?? "");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        /// <summary>
        /// Изменить статус интерьвью
        /// </summary>  
        public void dbUpdateState(int id, InterviewState state)
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[UpdateInterviewState] @id, @state";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@state", state);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        /// <summary>
        /// Удалить интерьвью из БД
        /// </summary>  
        public void dbDelete()
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[DeleteInterview] @id";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
    public enum InterviewState
    {
        Plan = 1,
        Canceled = 2,
        Finished = 3,
    }
}
