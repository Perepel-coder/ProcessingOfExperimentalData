using Services.Interfaces;
using System.Collections.Generic;
using MathNet.Numerics.Statistics;
using System.Data;
using System;
using Accord.Statistics.Analysis;
using Accord.Statistics.Testing;

namespace Services
{
    public class StatisticalAnalysis: IStatisticalAnalysis
    {
        private static int round = 6;
        public DataTable StatisticalAnalysisREPORT(IEnumerable<double> samples, int degreesOfFreedom)
        {
            var chiSquareTest = new ChiSquareTest(samples.Kurtosis(), degreesOfFreedom);

            var samplesArr = new List<double>(samples);
            DataTable table = new DataTable();
            table.Columns.Add("Описание");
            table.Columns.Add("Значение");

            table.Rows.Add(new object[2] { "Критерий согласия Пирсона (хи-квадрат)", chiSquareTest.PValue });
            table.Rows.Add(new object[2] { "Нулевую гипотезу следует отклонить?", chiSquareTest.Significant });
            table.Rows.Add(new object[2] { "Среднее значение распределения", chiSquareTest.StatisticDistribution.Mean });
            table.Rows.Add(new object[2] { "Стандартное отклонение", Math.Round(Statistics.StandardDeviation(samplesArr), round) });
            table.Rows.Add(new object[2] { "Среднее гармоническое", Math.Round(samplesArr.HarmonicMean(), round) });
            table.Rows.Add(new object[2] { "Ассиметрия", Math.Round(samplesArr.Skewness(), round) });
            table.Rows.Add(new object[2] { "Дисперсия", Math.Round(Statistics.Variance(samplesArr), round) });
            table.Rows.Add(new object[2] { "Энтропия", Math.Round(Statistics.Entropy(samplesArr), round) });
            table.Rows.Add(new object[2] { "Эксцесс", Math.Round(samplesArr.Kurtosis(), round) });

            return table;
        }
        public DataTable CorrelationREPOST(IEnumerable<double> firstFactor, IEnumerable<double> secondFactor)
        {
            var kolmogorovSmirnovTest = new TwoSampleKolmogorovSmirnovTest((new List<double>(firstFactor)).ToArray(), (new List<double>(secondFactor)).ToArray());

            DataTable table = new DataTable();
            table.Columns.Add("Описание");
            table.Columns.Add("Значение");

            table.Rows.Add(new object[2] { "Коэффициент корреляции Пирсона", Math.Round(Correlation.Pearson(firstFactor, secondFactor), round) });
            table.Rows.Add(new object[2] { "Коэффициент корреляции Спирмана", Math.Round(Correlation.Spearman(firstFactor, secondFactor), round) });
            table.Rows.Add(new object[2] { "Коэффициент корреляции Спирмана", Math.Round(Correlation.Spearman(firstFactor, secondFactor), round) });
            table.Rows.Add(new object[2] { "Теcт Колмагорова - Смирнова (хи-квадрат)", kolmogorovSmirnovTest.PValue });
            table.Rows.Add(new object[2] { "Теcт Колмагорова - Смирнова (статистика)", kolmogorovSmirnovTest.Statistic });

            table.Rows.Add(new object[2] { "Коввариация", Math.Round(firstFactor.Covariance(secondFactor), round) });

            return table;
        }
    }
}
