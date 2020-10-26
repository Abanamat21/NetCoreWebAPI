using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_2_0.Models.Interfaces;
using WebApi_2_0.Models.Services;

namespace WebApi_2_0.Models.InterfaceModels
{
    public class NewCandidateParams
    {
        public String CandidateName { get; set; }
        public String CandidatePhone { get; set; }
        public String CandidatePosision { get; set; }
        public String HRMail { get; set; }
        public DateTime InterviewDate { get; set; }
        public String InterviewerName { get; set; }
        public DateTime TaskDeadLine { get; set; }

        private static IDomainCaster domainCaster = new TSQLCaster();

        /// <summary>
        /// Знести данные о соискателе, запланировать собеседование и выдать задание
        /// </summary>
        public void InsertNewCandidate()
        {
            var candidate = new Candidate();
            candidate.Name = CandidateName;
            candidate.PhoneNumber = CandidatePhone;
            candidate.ExpectedPosition = CandidatePosision;
            candidate.HRMail = HRMail;
            domainCaster.GetACandidate(candidate).InsertNewCandidate(InterviewDate, InterviewerName, TaskDeadLine);
        }
    }
}
