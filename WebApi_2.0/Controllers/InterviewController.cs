using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRLib.Models.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using WebApi_2_0.Models;
using WebApi_2_0.Models.InterfaceModels;

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
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(Interview.SelectInterview(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Изменить статусдобавить интервью
        /// </summary>
        [HttpPost]
        public IActionResult Post(Interview interview)
        {
            try
            {
                interview.Insert();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Изменить статус интервью
        /// </summary>
        [HttpPut("{id}/{stateid}")]
        public IActionResult Put(int id, int stateid)
        {
            try
            {
                var oldInterview = Interview.SelectInterview(id);
                oldInterview.UpdateState(id, (InterviewState)stateid);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Удалить интервью
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var interview = Interview.SelectInterview(id);
                interview.Delete();
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
