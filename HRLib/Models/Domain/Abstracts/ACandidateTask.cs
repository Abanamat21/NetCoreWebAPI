using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRLib.Models.Domain.Enums;

namespace HRLib.Models.Domain.Abstracts
{
    /// <summary>
    /// Модель задачи соискателяю в некотором источнике данных
    /// </summary>
    public abstract class ACandidateTask
    {/// <summary>
     /// id, NOT NULL
     /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// candidate_id, NOT NULL
        /// </summary>
        public ACandidate Candidate { get; set; }
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

        /// <summary>
        /// Получить задачу из БД
        /// </summary>   
        public abstract void dbSelect(int id);

        /// <summary>
        /// Занести новую задачу в БД
        /// </summary> 
        public abstract void dbInsert();

        /// <summary>
        /// Отметить факт выполнения задачи
        /// </summary> 
        public abstract void dbUpdateCompleted(DateTime completionDTime);

        /// <summary>
        /// Установить задаче оценку
        /// </summary> 
        public abstract void dbUpdateChecked(String inspector, float rating);

        /// <summary>
        /// Удалить задачу из БД
        /// </summary>  
        public abstract void dbDelete();
    }
}
