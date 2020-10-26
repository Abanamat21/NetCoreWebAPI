using System;
using System.Data;
using System.Data.SqlClient;
using HRLib.Models.Domain.Enums;
using WebApi_2_0.Models.Interfaces;
using WebApi_2_0.Models.Services;

namespace WebApi_2_0.Models.InterfaceModels
{
    public class Interview
    {
        /// <summary>
        /// id из источника данных
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Кандидат, для которого делается интервью
        /// </summary>
        public Candidate Candidate { get; set; }
        /// <summary>
        /// Имя интервьювера
        /// </summary>
        public String InterviewerName { get; set; }
        /// <summary>
        /// Время проведения интервью
        /// </summary>
        public DateTime DTime { get; set; }
        /// <summary>
        /// Состояние интерьювью на текущий момент
        /// </summary>
        public InterviewState State { get; set; }

        private static IDomainCaster domainCaster = new TSQLCaster();

        public Interview()
        {
        }        

        public static Interview SelectInterview(int id)
        {
            return domainCaster.GetInterview(id);
        }

        public void Insert()
        {
            domainCaster.GetAInterview(this).dbInsert();
        }

        public void Delete()
        {
            domainCaster.GetAInterview(this).dbDelete();
        }

        internal void UpdateState(int id, InterviewState stateid)
        {
            domainCaster.GetAInterview(this).dbUpdateState(id, stateid);
        }
    }
}
