using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_2_0.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace WebApi_2_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        /// <summary>
        /// Знести данные о соискателе, запланировать собеседование и выдать задание
        /// </summary>
        [HttpPost("NewCandidate")]
        public void NewCandidatePost(newCandidateParams newCandidate)
        {
            newCandidate.InsertNewCandidate();
        }

        /// <summary>
        /// Отчет о результатах выполнения задач
        /// </summary>  
        [HttpGet("Report/{start}/{end}")]
        public List<ReportRow> ReportGet(DateTime start, DateTime end)
        {
            List<ReportRow> rows = new List<ReportRow>();
            rows = Candidate.CandidatesReport(start, end);
            return rows;
        }

        /// <summary>
        /// Полчить данные о соискателе
        /// </summary>        
        [HttpGet("{id}")]
        public Candidate Get(int id)
        {
            Candidate ret = new Candidate();
            ret = new Candidate(id);
            return ret;
        }

        /// <summary>
        /// Добавить соискателя
        /// </summary>  
        [HttpPost]
        public void Post(Candidate candidate)
        {
            candidate.dbInsert();
        }

        /// <summary>
        /// Удалить соискателя
        /// </summary>  
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Candidate candidate = new Candidate(id);
            candidate.dbDelete();
        }

    }
}
