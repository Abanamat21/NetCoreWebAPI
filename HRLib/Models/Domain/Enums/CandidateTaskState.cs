using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLib.Models.Domain.Enums
{
    /// <summary>
    /// Статус задачи соискателя
    /// </summary>
    public enum CandidateTaskState
    {
        /// <summary>
        /// Отправлено
        /// </summary>
        Sended = 1,

        /// <summary>
        /// Завершено
        /// </summary>
        Finished = 2,

        /// <summary>
        /// Проверено
        /// </summary>
        Checked = 3
    }
}
