using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi_2_0.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_2_0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateTaskController : ControllerBase
    {
        /// <summary>
        /// Получить данные об задаче
        /// </summary>
        [HttpGet("{id}")]
        public CandidateTask Get(int id)
        {
            CandidateTask ret = new CandidateTask();
            ret = new CandidateTask(id);
            return ret;
        }

        /// <summary>
        /// Добавить новую задачу
        /// </summary>
        [HttpPost]
        public void Post(CandidateTask candidateTask)
        {
            candidateTask.dbInsert();
            Candidate candidate = new Candidate(candidateTask.ID);
            EMailWorcker.sendEMail("Соискателю выдана задача", $"Выдана задача соискателю {candidate.Name} крайний срок выполнения {candidateTask.ExpectedCompletionDate}.", new List<string>() { candidate.HRMail });
        }

        /// <summary>
        /// Сообщить о выполнении задачи
        /// </summary>
        [HttpPut("completed/{id}/{completiondtime}")]
        public void SetCompletedPut(int id, DateTime completiondtime)
        {
            CandidateTask oldCandidateTask = new CandidateTask(id);
            oldCandidateTask.dbUpdateCompleted(completiondtime);
            EMailWorcker.sendEMail("Соискатель выполнил задачу", $"Соискатель {oldCandidateTask.Candidate.Name} выполнил задачу {oldCandidateTask.ID} и сдал на проверку.", new List<string>() { oldCandidateTask.Candidate.HRMail });
        }

        /// <summary>
        /// Оценить задачу
        /// </summary>
        [HttpPut("checked/{id}/{inspector}/{rating}")]
        public void SetCheckedPut(int id, String inspector, float rating)
        {
            CandidateTask oldCandidateTask = new CandidateTask(id);
            oldCandidateTask.dbUpdateChecked(inspector, rating);
            EMailWorcker.sendEMail("Определена оценка выполненой задачи", $"{inspector} выполнил задачувыстовил оценку {rating} задаче {oldCandidateTask.ID}, выполненой соискателем {oldCandidateTask.Candidate.Name}.", new List<string>() { oldCandidateTask.Candidate.HRMail });
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            CandidateTask candidateTask = new CandidateTask(id);
            candidateTask.dbDelete();
        }
    }
}
