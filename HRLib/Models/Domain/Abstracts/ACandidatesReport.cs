using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLib.Models.Domain.Abstracts
{
    /// <summary>
    /// Модель отчета по соискателям в некотором источнике данных
    /// </summary>
    public abstract class ACandidatesReport
    {
        /// <summary>
        /// Строки отчета
        /// </summary>
        public List<ACandidatesReportRow> Rows { get; set; }

        /// <summary>
        /// Отчет о результатах выполнения задач
        /// </summary>   
        public abstract void fillReport(DateTime start, DateTime end);
    }
}
