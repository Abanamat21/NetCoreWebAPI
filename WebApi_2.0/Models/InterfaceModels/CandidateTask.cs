using HRLib.Models.Domain.Enums;
using System;
using System.Data;
using System.Data.SqlClient;
using WebApi_2_0.Models.Interfaces;
using WebApi_2_0.Models.Services;

namespace WebApi_2_0.Models.InterfaceModels
{
    public class CandidateTask
    {
        /// <summary>
        /// id, NOT NULL
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// candidate_id, NOT NULL
        /// </summary>
        public Candidate Candidate { get; set; }
        /// <summary>
        /// descr, NULL
        /// </summary>
        public String Descr { get; set; }
        /// <summary>
        /// inspector, NULL
        /// </summary>
        public String InspectorName { get; set; }
        /// <summary>
        /// receipt_dtime, NOT NULL
        /// </summary>
        public DateTime ReceiptDate { get; set; }
        /// <summary>
        /// expected_completion_dtime, NOT NULL
        /// </summary>
        public DateTime ExpectedCompletionDate { get; set; }
        /// <summary>
        /// completion_dtime, NULL
        /// </summary>
        public DateTime FactCompletionDate { get; set; }
        /// <summary>
        /// status_id, NOT NULL
        /// </summary>
        public CandidateTaskState State { get; set; }
        /// <summary>
        /// inspector_rating, NULL
        /// </summary>
        public double InspectorRating { get; set; }

        private static IDomainCaster domainCaster = new TSQLCaster();

        public CandidateTask()
        {
        }

        public static CandidateTask SelectCandidateTask(int id)
        {
            return domainCaster.GetCandidateTask(id);
        }

        public void Insert()
        {
            domainCaster.GetACandidateTask(this).dbInsert();
        }

        public void Delete()
        {
            domainCaster.GetACandidateTask(this).dbDelete();
        }

        public void UpdateCompleted(DateTime completiondtime)
        {
            domainCaster.GetACandidateTask(this).dbUpdateCompleted(completiondtime);
        }

        public void UpdateChecked(String inspector, float rating)
        {
            domainCaster.GetACandidateTask(this).dbUpdateChecked(inspector, rating);
        }
    }
}
