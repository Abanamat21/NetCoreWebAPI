using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_2_0.Models.Interfaces;

namespace WebApi_2_0.Models.Services
{
    public class EMailWorcker : IEMailWorcker
    {
        public void sendEMail(String subject, String body, List<String> recipients)
        {
            //Очень продуманный код, который, наверное, обращается к внешнему сервису.
        }
    }
}
