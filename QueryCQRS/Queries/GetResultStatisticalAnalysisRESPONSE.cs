using Services.Interfaces;
using System.Collections.Generic;
using System.Data;

namespace QueryCQRS.Queries
{
    public class GetResultStatisticalAnalysisRESPONSE
    {
        public DataTable ReportStatisticalAnalysis { get; private set; }
        public GetResultStatisticalAnalysisRESPONSE(IStatisticalAnalysis analysis, IEnumerable<double> samples, int degreesOfFreedom)
        {
            this.ReportStatisticalAnalysis = analysis.StatisticalAnalysisREPORT(samples, degreesOfFreedom);
        }
        public GetResultStatisticalAnalysisRESPONSE(IStatisticalAnalysis analysis, IEnumerable<double> firstFactor, IEnumerable<double> secondFactor)
        {
            this.ReportStatisticalAnalysis = analysis.CorrelationREPOST(firstFactor, secondFactor);
        }
    }
}
