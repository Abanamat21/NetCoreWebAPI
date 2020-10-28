using System;
using System.Collections.Generic;

namespace HRLib.Interfaces
{
    /// <summary>
    /// Интерфейс взаимодействия с сервисом по отправке писем
    /// </summary>
    public interface IEMailWorcker
    {
        /// <summary>
        /// Отправка письма на список адресов recipients с темой subject и телом body
        /// </summary>
        void sendEMail(String subject, String body, List<String> recipients);
    }
}