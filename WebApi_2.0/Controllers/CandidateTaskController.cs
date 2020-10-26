using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi_2_0.Models.InterfaceModels;
using WebApi_2_0.Models.Interfaces;
using WebApi_2_0.Models.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_2_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateTaskController : ControllerBase
    {
        public IEMailWorcker eMailWorcker = new EMailWorcker();

        /// <summary>
        /// Получить данные об задаче
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(CandidateTask.SelectCandidateTask(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Добавить новую задачу
        /// </summary>
        [HttpPost]
        public IActionResult Post(CandidateTask candidateTask)
        {
            try
            {
                candidateTask.Insert();
                var candidate = Candidate.SelectCandidate(candidateTask.ID);
                eMailWorcker.sendEMail("Соискателю выдана задача", $"Выдана задача соискателю {candidate.Name} крайний срок выполнения {candidateTask.ExpectedCompletionDate}.", new List<string>() { candidate.HRMail });
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Сообщить о выполнении задачи
        /// </summary>
        [HttpPut("completed/{id}/{completiondtime}")]
        public IActionResult SetCompletedPut(int id, DateTime completiondtime)
        {
            try
            {
                var oldCandidateTask = CandidateTask.SelectCandidateTask(id);
                oldCandidateTask.UpdateCompleted(completiondtime);
                eMailWorcker.sendEMail("Соискатель выполнил задачу", $"Соискатель {oldCandidateTask.Candidate.Name} выполнил задачу {oldCandidateTask.ID} и сдал на проверку.", new List<string>() { oldCandidateTask.Candidate.HRMail });
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Оценить задачу
        /// </summary>
        [HttpPut("checked/{id}/{inspector}/{rating}")]
        public IActionResult SetCheckedPut(int id, String inspector, float rating)
        {
            try
            {
                var oldCandidateTask = CandidateTask.SelectCandidateTask(id);
                oldCandidateTask.UpdateChecked(inspector, rating);
                eMailWorcker.sendEMail("Определена оценка выполненой задачи", $"{inspector} выполнил задачувыстовил оценку {rating} задаче {oldCandidateTask.ID}, выполненой соискателем {oldCandidateTask.Candidate.Name}.", new List<string>() { oldCandidateTask.Candidate.HRMail });
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var candidateTask = CandidateTask.SelectCandidateTask(id);
                candidateTask.Delete();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
