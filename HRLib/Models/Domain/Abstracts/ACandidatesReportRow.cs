using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLib.Models.Domain.Abstracts
{
    /// <summary>
    /// Модель строки отчета по соискателям в некотором источнике данных
    /// </summary>
    public abstract class ACandidatesReportRow
    {
        /// <summary>
        /// Имя соискателя
        /// </summary>
        public String candidateName { get; set; }

        /// <summary>
        /// Должность соискателя
        /// </summary>
        public String candidatePosision { get; set; }

        /// <summary>
        /// Оценка последней задачи.
        /// </summary>
        public String lastTaskRating { get; set; }
    }
}
