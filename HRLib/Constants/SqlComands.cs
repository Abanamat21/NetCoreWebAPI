using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLib.Constants
{
    public static class SqlCommands
    {
        public const String SelectCandidate      = "SELECT top 1 * FROM [dbo].[SelectCandidate] (@id)";
        public const String CandidatesReport     = "SELECT candidateid, candidate, position, rating FROM [dbo].[CandidatesReport] (@start, @end)";
        public const String InsertCandidate      = "EXECUTE [dbo].[InsertCandidate] @candidate, @phone_number, @position, @hr_mail";
        public const String NewCandidate         = "EXECUTE [dbo].[NewCandidate] @candidate, @phone_number, @position, @hr_mail, @dtime, @interviewer, @expected_completion_dtime";
        public const String DeleteCandidate      = "EXECUTE [dbo].[DeleteCandidate] @id";

        public const String SelectInterview      = "SELECT top 1 * FROM [dbo].[SelectInterview] (@id)";
        public const String InsertInterview      = "EXECUTE [dbo].[InsertInterview] @candidate_id, @dtime, @interviewer";
        public const String UpdateInterviewState = "EXECUTE [dbo].[UpdateInterviewState] @id, @state";
        public const String DeleteInterview      = "EXECUTE [dbo].[DeleteInterview] @id";

        public const String SelectCandidateTask          = "SELECT top 1 * FROM [dbo].[SelectCandidateTask] (@id)";
        public const String InsertCandidateTask          = "EXECUTE [dbo].[InsertCandidateTask] @candidate_id, @descr, @dtime, @expected_completion_dtime";
        public const String UpdateCandidateTaskCompleted = "EXECUTE [dbo].[UpdateCandidateTaskCompleted] @id, @completiondtime";
        public const String UpdateCandidateTaskChecked   = "EXECUTE [dbo].[UpdateCandidateTaskChecked] @id, @inspector, @rating";
        public const String DeleteCandidateTask          = "EXECUTE [dbo].[DeleteCandidateTask] @id";
    }
}
