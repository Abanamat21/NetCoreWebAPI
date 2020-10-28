using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_2_0.Models.InterfaceModels
{
    public class CandidatesReportRow
    {
        /// <summary>
        /// ИД соискателя
        /// </summary>
        public int candidateId { get; set; }

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
