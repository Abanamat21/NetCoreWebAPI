using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRLib.Models.Domain.Enums;

namespace HRLib.Models.Domain.Abstracts
{
    /// <summary>
    /// Модель интервью в некотором источнике данных
    /// </summary>
    public abstract class AInterview
    {/// <summary>
     /// id, NOT NULL
     /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// candidate_id, NOT NULL
        /// </summary>
        public ACandidate Candidate { get; set; }
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


        /// <summary>
        /// Получить интерьвью из БД
        /// </summary>   
        public abstract void dbSelect(int id);
        /// <summary>
        /// Занести новое интерьвью в БД
        /// </summary>  
        public abstract void dbInsert();
        /// <summary>
        /// Изменить статус интерьвью
        /// </summary>  
        public abstract void dbUpdateState(int id, InterviewState state);
        /// <summary>
        /// Удалить интерьвью из БД
        /// </summary>  
        public abstract void dbDelete();
    }
}
