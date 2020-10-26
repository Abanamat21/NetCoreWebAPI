using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HRLib.Models.Domain.Abstracts;
using HRLib.Models.Domain.Enums;
using HRLib.Constants;

namespace HRLib.Models.Domain.TSQL
{
    public class TSQLInterview : AInterview
    {
        public TSQLInterview()
        {
        }

        public TSQLInterview(int id)
        {
            dbSelect(id);
        }

        /// <summary>
        /// Получить интерьвью из БД
        /// </summary>   
        public override void dbSelect(int id)
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.SelectInterview, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ID = dr.GetInt32("id");
                    Candidate = new TSQLCandidate(dr.GetInt32("candidate_id"));
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
        public override void dbInsert()
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.InsertInterview, conn);
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
        public override void dbUpdateState(int id, InterviewState state)
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmd = new SqlCommand(SqlCommands.UpdateInterviewState, conn);
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
        public override void dbDelete()
        {
            using (var conn = new SqlConnection(SqlConnStrings.crmConnString))
            {
                var cmdStr = SqlCommands.DeleteInterview;
                var cmd = new SqlCommand(cmdStr, conn);
                cmd.Parameters.AddWithValue("@id", ID);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
