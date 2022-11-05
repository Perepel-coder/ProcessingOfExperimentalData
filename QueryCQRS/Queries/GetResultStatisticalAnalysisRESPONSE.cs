using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryCQRS.Queries
{
    public class GetResultStatisticalAnalysisRESPONSE
    {
        public double Moda { get; set; }
        public double Variance { get; set; }
        public double MeanSquareDeviation { get; set; }
        public double PearsonAsymmetryCoefficient { get; set; }
        public double AsymmetryCoefficient { get; set; }
        public double Excess { get; set; }
        public double HarkeberStatistics { get; set; }
        public double PearsonCriterionConsent { get; set; }

        public void GenerateReport(IStatisticalAnalysis analysis)
        {
            this.Moda = analysis.FindModa();
            this.Variance = analysis.FindCentralMoment(2);
            this.MeanSquareDeviation = analysis.FindMeanSquareDeviation();
            this.PearsonAsymmetryCoefficient = analysis.FindPearsonAsymmetryCoefficient();
            this.AsymmetryCoefficient = analysis.FindAsymmetryCoefficient();
            this.Excess = analysis.FindExcess();
            this.HarkeberStatistics = analysis.FindHarkeberStatistics();
            this.PearsonCriterionConsent = analysis.FindPearsonCriterionConsent();
        }
    }
}
