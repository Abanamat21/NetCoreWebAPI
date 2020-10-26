using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRLib.Models.Domain.Abstracts;
using HRLib.Models.Domain.TSQL;
using WebApi_2_0.Models.InterfaceModels;
using WebApi_2_0.Models.Interfaces;

namespace WebApi_2_0.Models.Services
{
    public class TSQLCaster : IDomainCaster
    {
        public Candidate GetCandidate(ACandidate aCandidate)
        {
            Candidate candidate = new Candidate();
            candidate.ID = aCandidate.ID;
            candidate.Name = aCandidate.Name;
            candidate.PhoneNumber = aCandidate.PhoneNumber;
            candidate.ExpectedPosition = aCandidate.ExpectedPosition;
            candidate.HRMail = aCandidate.HRMail;
            return candidate;
        }

        public Interview GetInterview(AInterview aInterview)
        {
            Interview interview = new Interview();
            interview.ID = aInterview.ID;
            interview.Candidate = GetCandidate(aInterview.Candidate);
            interview.DTime = aInterview.DTime;
            interview.InterviewerName = aInterview.InterviewerName;
            interview.State = aInterview.State;
            return interview;
        }

        public CandidateTask GetCandidateTask(ACandidateTask aCandidateTask)
        {
            CandidateTask candidateTask = new CandidateTask();
            candidateTask.ID = aCandidateTask.ID;
            candidateTask.Candidate = GetCandidate(aCandidateTask.Candidate);
            candidateTask.Descr = aCandidateTask.Descr;
            candidateTask.ExpectedCompletionDate = aCandidateTask.ExpectedCompletionDate;
            candidateTask.FactCompletionDate = aCandidateTask.FactCompletionDate;
            candidateTask.InspectorName = aCandidateTask.InspectorName;
            candidateTask.InspectorRating = aCandidateTask.InspectorRating;
            candidateTask.ReceiptDate = aCandidateTask.ReceiptDate;
            candidateTask.State = aCandidateTask.State;
            return candidateTask;
        }
        public ACandidate GetACandidate(Candidate candidate)
        {
            ACandidate aCandidate = new TSQLCandidate();
            aCandidate.ID = candidate.ID;
            aCandidate.Name = candidate.Name;
            aCandidate.PhoneNumber = candidate.PhoneNumber;
            aCandidate.ExpectedPosition = candidate.ExpectedPosition;
            aCandidate.HRMail = candidate.HRMail;
            return aCandidate;
        }

        public AInterview GetAInterview(Interview interview)
        {
            AInterview aInterview = new TSQLInterview();
            aInterview.ID = interview.ID;
            aInterview.Candidate = GetACandidate(interview.Candidate);
            aInterview.DTime = interview.DTime;
            aInterview.InterviewerName = interview.InterviewerName;
            aInterview.State = interview.State;
            return aInterview;
        }

        public ACandidateTask GetACandidateTask(CandidateTask candidateTask)
        {
            ACandidateTask aCandidateTask = new TSQLCandidateTask();
            aCandidateTask.ID = candidateTask.ID;
            aCandidateTask.Candidate = GetACandidate(candidateTask.Candidate);
            aCandidateTask.Descr = candidateTask.Descr;
            aCandidateTask.ExpectedCompletionDate = candidateTask.ExpectedCompletionDate;
            aCandidateTask.FactCompletionDate = candidateTask.FactCompletionDate;
            aCandidateTask.InspectorName = candidateTask.InspectorName;
            aCandidateTask.InspectorRating = candidateTask.InspectorRating;
            aCandidateTask.ReceiptDate = candidateTask.ReceiptDate;
            aCandidateTask.State = candidateTask.State;
            return aCandidateTask;
        }

        public ACandidate GetACandidate(int id)
        {
            return new TSQLCandidate(id);
        }

        public AInterview GetAInterview(int id)
        {
            return new TSQLInterview(id);
        }

        public ACandidateTask GetACandidateTask(int id)
        {
            return new TSQLCandidateTask(id);
        }

        public Candidate GetCandidate(int id)
        {
            return GetCandidate(new TSQLCandidate(id));
        }

        public Interview GetInterview(int id)
        {
            return GetInterview(new TSQLInterview(id));
        }

        public CandidateTask GetCandidateTask(int id)
        {
            return GetCandidateTask(new TSQLCandidateTask(id));
        }

        public CandidatesReportRow GetCandidatesReportRow(ACandidatesReportRow aCandidatesReportRow)
        {
            CandidatesReportRow row = new CandidatesReportRow();
            row.candidateName = aCandidatesReportRow.candidateName;
            row.candidatePosision = aCandidatesReportRow.candidatePosision;
            row.lastTaskRating = aCandidatesReportRow.lastTaskRating;
            return row;
        }

        public ACandidatesReport GetCandidatesReport(DateTime start, DateTime end)
        {
            ACandidatesReport report = new TSQLCandidatesReport();
            report.fillReport(start, end);
            return report;
        }
    }

}
