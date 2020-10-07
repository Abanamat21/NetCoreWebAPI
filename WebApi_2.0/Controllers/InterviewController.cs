using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using WebApi_2_0.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_2_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : ControllerBase
    {
        /// <summary>
        /// Получить данные об интервью
        /// </summary>
        [HttpGet("{id}")]
        public Interview Get(int id)
        {
            Interview ret = new Interview();
            ret = new Interview(id);
            return ret;
        }

        /// <summary>
        /// Изменить статусдобавить интервью
        /// </summary>
        [HttpPost]
        public void Post(Interview interview)
        {
            interview.dbInsert();
        }

        /// <summary>
        /// Изменить статус интервью
        /// </summary>
        [HttpPut("{id}/{stateid}")]
        public void Put(int id, int stateid)
        {
            Interview oldInterview = new Interview(id);
            oldInterview.dbUpdateState(id, (InterviewState)stateid);
        }

        /// <summary>
        /// Удалить интервью
        /// </summary>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Interview interview = new Interview(id);
            interview.dbDelete();
        }
    }
}
