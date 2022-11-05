using MediatR;
using Services.Interfaces;

namespace QueryCQRS.Queries
{
    public class GetResultStatisticalAnalysisREQUEST : IRequest<GetResultStatisticalAnalysisRESPONSE>
    {
        private readonly IStatisticalAnalysis analysis;
        public GetResultStatisticalAnalysisREQUEST(IStatisticalAnalysis analysis)
        {
            this.analysis = analysis;
        }
        public GetResultStatisticalAnalysisRESPONSE GenerateReport()
        {
            var report = new GetResultStatisticalAnalysisRESPONSE();
            report.GenerateReport(this.analysis);
            return report;
        }
    }
}
