using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IStatisticalAnalysis
    {
        double FindModa();
        double FindCentralMoment(int pow);
        double FindMeanSquareDeviation();
        double FindPearsonAsymmetryCoefficient();
        double FindAsymmetryCoefficient();
        double FindExcess();
        double FindHarkeberStatistics();
        double FindPearsonCriterionConsent();
    }
}
