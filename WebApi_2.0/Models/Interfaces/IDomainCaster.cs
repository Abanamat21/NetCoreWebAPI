using HRLib.Models.Domain.Abstracts;
using System;
using WebApi_2_0.Models.InterfaceModels;

namespace WebApi_2_0.Models.Interfaces
{
    /// <summary>
    /// Интерфейс для реализации классов преобразования моделей источника данных к моделям веб-взаимодействия и обратно
    /// </summary>
    public interface IDomainCaster
    {
        public Candidate GetCandidate(ACandidate aCandidate);
        public Interview GetInterview(AInterview aInterview);
        public CandidateTask GetCandidateTask(ACandidateTask aCandidateTask);
        public CandidatesReportRow GetCandidatesReportRow(ACandidatesReportRow aCandidatesReportRow);
        public ACandidatesReport GetCandidatesReport(DateTime start, DateTime end);

        public Candidate GetCandidate(int id);
        public Interview GetInterview(int id);
        public CandidateTask GetCandidateTask(int id);

        public ACandidate GetACandidate(Candidate Candidate);
        public AInterview GetAInterview(Interview Interview);
        public ACandidateTask GetACandidateTask(CandidateTask CandidateTask);

        public ACandidate GetACandidate(int id);
        public AInterview GetAInterview(int id);
        public ACandidateTask GetACandidateTask(int id);
    }
}