using System;
using System.Collections.Generic;
using HRLib.Interfaces;
using Flurl;
using Flurl.Http;
using System.Threading.Tasks;

namespace HRLib.Services
{
    public class EMailWorcker : IEMailWorcker
    {
        public void sendEMail(String subject, String body, List<String> recipients)
        {
            //Очень продуманный код, который, наверное, обращается к внешнему сервису.            
            var getResp = $"http://SmartMailSender/Send".PostJsonAsync(new { Subject = subject, Body = body, Recipients = recipients });
            var x = getResp.Result;

        }
    }
}
