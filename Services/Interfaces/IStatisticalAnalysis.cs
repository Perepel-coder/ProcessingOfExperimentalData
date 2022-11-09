using System.Collections.Generic;
using System.Data;

namespace Services.Interfaces
{
    public interface IStatisticalAnalysis
    {
        DataTable StatisticalAnalysisREPORT(IEnumerable<double> samples, int degreesOfFreedom);
        DataTable CorrelationREPOST(IEnumerable<double> firstFactor, IEnumerable<double> secondFactor);
    }
}
