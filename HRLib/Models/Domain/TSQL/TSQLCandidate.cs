using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HRLib.Constants;
using HRLib.Models.Domain.Abstracts;

namespace HRLib.Models.Domain.TSQL
{
    public class TSQLCandidate : ACandidate
    {
        public TSQLCandidate()
        {
        }
        public TSQLCandidate(int id)
        {
            dbSelect(id);
        }

        /// <summary>
        /// Получить соискателя из БД
        /// </summary>        
        public override void dbSelect(int id)
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.SelectCandidate, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                var dr = cmd.ExecuteReader();
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
        public override void dbInsert()
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.InsertCandidate, conn);
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
        public override void dbDelete()
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.DeleteCandidate, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        /// <summary>
        /// Знести данные о соискателе, запланировать собеседование и выдать задание
        /// </summary>
        public override void InsertNewCandidate(DateTime interviewDTime, String interviewInterviewerName, DateTime candidateTaskExpectedCompletionDate)
        {
            if (candidateTaskExpectedCompletionDate == DateTime.MinValue) throw new ArgumentException("taskDeadLine is not seted");

            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.NewCandidate, conn);
                cmd.Parameters.AddWithValue("@candidate", Name ?? "");
                cmd.Parameters.AddWithValue("@phone_number", PhoneNumber ?? "");
                cmd.Parameters.AddWithValue("@position", ExpectedPosition ?? "");
                cmd.Parameters.AddWithValue("@hr_mail", HRMail ?? "");
                cmd.Parameters.AddWithValue("@dtime", interviewDTime == DateTime.MinValue ? DateTime.Now : interviewDTime);
                cmd.Parameters.AddWithValue("@interviewer", interviewInterviewerName ?? "");
                cmd.Parameters.AddWithValue("@expected_completion_dtime", candidateTaskExpectedCompletionDate);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
