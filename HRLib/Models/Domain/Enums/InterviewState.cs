using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLib.Models.Domain.Enums
{
    /// <summary>
    /// Статус интервью соискателя
    /// </summary>
    public enum InterviewState
    {
        /// <summary>
        /// Запланировано
        /// </summary>
        Plan = 1,

        /// <summary>
        /// Отменено
        /// </summary>
        Canceled = 2,

        /// <summary>
        /// Завершено
        /// </summary>
        Finished = 3,
    }
}
