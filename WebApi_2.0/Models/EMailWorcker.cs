using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_2_0.Models
{
    public class EMailWorcker
    {
        /// <summary>
        /// Отправка письма на список адресов recipients с темой subject и телом body
        /// </summary>
        public static void sendEMail(String subject, String body, List<String> recipients)
        {
            //Очень продуманный код, который, наверное, обращается к внешнему сервису.
        }
    }
}
