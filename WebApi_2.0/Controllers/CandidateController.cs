using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_2_0.Models.InterfaceModels;
using Newtonsoft.Json;
using System.Diagnostics;
using WebApi_2_0.Models.Interfaces;
using WebApi_2_0.Models.Services;

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
        public IActionResult NewCandidatePost(NewCandidateParams newCandidate)
        {
            try
            {
                newCandidate.InsertNewCandidate();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Отчет о результатах выполнения задач
        /// </summary>  
        [HttpGet("Report/{start}/{end}/{candidateId}")]
        public IActionResult ReportGet(DateTime start, DateTime end, int candidateId)
        {
            try
            {
                var report = new CandidatesReport(start, end);
                if (candidateId != 0)
                    report.rows = report.rows.Where(x => x.candidateId == candidateId).ToList();
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Полчить данные о соискателе
        /// </summary>        
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(Candidate.SelectCandidate(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Добавить соискателя
        /// </summary>  
        [HttpPost]
        public IActionResult Post(Candidate candidate)
        {
            try
            {
                candidate.Insert();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Удалить соискателя
        /// </summary>  
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var candidate = Candidate.SelectCandidate(id);
                candidate.Delete();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
