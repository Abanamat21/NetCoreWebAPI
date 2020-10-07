using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace WebApi_2_0.Models
{
    public class Candidate
    {
        /// <summary>
        /// id, NOT NULL
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// candidate, NOT NULL
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// phone_number, NULL
        /// </summary>
        public String PhoneNumber { get; set; }
        /// <summary>
        /// position, NOT NULL
        /// </summary>
        public String ExpectedPosition { get; set; }
        /// <summary>
        /// hr_mail, NULL
        /// </summary>
        public String HRMail { get; set; }

        public Candidate(int id)
        {
            dbSelect(id);
        }
        public Candidate()
        {
        }

        /// <summary>
        /// Получить соискателя из БД
        /// </summary>        
        private void dbSelect(int id)
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "SELECT top 1 * FROM [dbo].[SelectCandidate] (@id)";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ID = dr.GetInt32("id");
                    Name = dr.GetString("candidate");
                    PhoneNumber = dr.IsDBNull("phone_number") ? "" : dr.GetString("phone_number");
                    ExpectedPosition = dr.GetString("position");
                    HRMail = dr.IsDBNull("hr_mail") ? "" : dr.GetString("hr_mail");
                }
                conn.Close();
            }
        }
        /// <summary>
        /// Занести нового соискателя в БД
        /// </summary>  
        public void dbInsert()
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[InsertCandidate] @candidate, @phone_number, @position, @hr_mail";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@candidate", Name ?? "");
                cmd.Parameters.AddWithValue("@phone_number", PhoneNumber ?? "");
                cmd.Parameters.AddWithValue("@position", ExpectedPosition ?? "");
                cmd.Parameters.AddWithValue("@hr_mail", HRMail ?? "");
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        /// <summary>
        /// Удалить соискателя из БД
        /// </summary>   
        public void dbDelete()
        {
            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[DeleteCandidate] @id";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        /// <summary>
        /// Отчет о результатах выполнения задач
        /// </summary>        
        public static List<ReportRow> CandidatesReport(DateTime start, DateTime end)
        {
            List<ReportRow> ret = new List<ReportRow>();

            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "SELECT candidate, position, rating FROM [dbo].[CandidatesReport] (@start, @end)";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@start", start);
                cmd.Parameters.AddWithValue("@end", end);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ret.Add(new ReportRow() 
                    { 
                        candidateName = dr.IsDBNull("candidate") ? "" : dr.GetString("candidate"), 
                        candidatePosision = dr.IsDBNull("position") ? "" : dr.GetString("position"),
                        lastTaskRating = dr.IsDBNull("rating") ? "" : dr.GetDouble("rating").ToString()
                    });
                }
                conn.Close();
            }
            return ret;
        }

    }


    public class newCandidateParams
    {
        public String CandidateName { get; set; }
        public String CandidatePhone { get; set; }
        public String CandidatePosision { get; set; }
        public String HRMail { get; set; }
        public DateTime InterviewDate { get; set; }
        public String InterviewerName { get; set; }
        public DateTime TaskDeadLine { get; set; }

        /// <summary>
        /// Знести данные о соискателе, запланировать собеседование и выдать задание
        /// </summary>
        public void InsertNewCandidate()
        {
            if (TaskDeadLine == DateTime.MinValue) throw new ArgumentException("taskDeadLine is not seted");

            using (SqlConnection conn = new SqlConnection(Constants.crmConnString))
            {
                String cmdStr = "EXECUTE [dbo].[NewCandidate] @candidate, @phone_number, @position, @hr_mail, @dtime, @interviewer, @expected_completion_dtime";
                SqlCommand cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@candidate", CandidateName ?? "");
                cmd.Parameters.AddWithValue("@phone_number", CandidatePhone ?? "");
                cmd.Parameters.AddWithValue("@position", CandidatePosision ?? "");
                cmd.Parameters.AddWithValue("@hr_mail", HRMail ?? "");
                cmd.Parameters.AddWithValue("@dtime", InterviewDate == DateTime.MinValue ? DateTime.Now : InterviewDate);
                cmd.Parameters.AddWithValue("@interviewer", InterviewerName ?? "");
                cmd.Parameters.AddWithValue("@expected_completion_dtime", TaskDeadLine);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
    public class ReportRow
    {
        public String candidateName;
        public String candidatePosision;
        public String lastTaskRating;
    }
}
