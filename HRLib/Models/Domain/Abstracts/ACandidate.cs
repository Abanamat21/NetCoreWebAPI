using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLib.Models.Domain.Abstracts
{
    /// <summary>
    /// Модель соискателя в некотором источнике данных
    /// </summary>
    public abstract class ACandidate
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

        /// <summary>
        /// Получить соискателя из БД
        /// </summary>        
        public abstract void dbSelect(int id);
        /// <summary>
        /// Занести нового соискателя в БД
        /// </summary>  
        public abstract void dbInsert();
        /// <summary>
        /// Удалить соискателя из БД
        /// </summary>   
        public abstract void dbDelete();
        /// <summary>
        /// Занести нового соискателя, интервью и первую задачу в БД
        /// </summary>  
        public abstract void InsertNewCandidate(DateTime interviewDTime, String interviewInterviewerName, DateTime candidateTaskExpectedCompletionDate);
    }
}
