using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_2_0.Models.Interfaces;
using WebApi_2_0.Models.Services;

namespace WebApi_2_0.Models.InterfaceModels
{
    public class CandidatesReport
    {
        public List<CandidatesReportRow> rows = new List<CandidatesReportRow>();

        private static IDomainCaster domainCaster = new TSQLCaster();

        public CandidatesReport(DateTime start, DateTime end)
        {
            var report = domainCaster.GetCandidatesReport(start, end);
            foreach(var row in report.Rows)
            {
                rows.Add(domainCaster.GetCandidatesReportRow(row));
            }
        }
    }
}
