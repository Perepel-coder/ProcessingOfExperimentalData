using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class StatisticalAnalysis: IStatisticalAnalysis
    {
        public List<CalculationTable> Intervals { get; }
        public double Step { get; }
        public double Middle { get; }
        public int NumExperiments { get; }
        public StatisticalAnalysis(IEnumerable<double> arr, double step, int numExperiments)
        {
            this.Step = step;
            this.NumExperiments = numExperiments;
            this.Middle = arr.Sum() / arr.Count();
            int size = arr.Count();
            arr = arr.OrderBy(p => p);
            double border = arr.First() + step;
            List<double> cup = new List<double>();
            foreach (double elm in arr)
            {
                if (elm <= border) { cup.Add(elm); }
                else
                {
                    if (cup.Count != 0)
                    {
                        Intervals.Add(new CalculationTable
                        {
                            LeftBorder = cup.Min(),
                            RightBorder = cup.Max(),
                            MiddleOfInterval = cup.Sum() / cup.Count,
                            Frequency = cup.Count
                        });
                    }
                    cup.Clear();
                    border += step;
                }
            }
        }

        /// <summary>
        /// мода
        /// </summary>
        /// <returns></returns>
        public double FindModa()
        {
            double maxFrequency = double.MinValue;
            int i;
            for (i = 0; i < Intervals.Count(); i++)
            {
                if (Intervals[i].Frequency > maxFrequency)
                {
                    maxFrequency = Intervals[i].Frequency;
                }
            }
            double intermediate1 = this.Intervals[i].Frequency - this.Intervals[i - 1].Frequency;
            double intermediate2 = this.Intervals[i].Frequency - this.Intervals[i + 1].Frequency;
            return this.Intervals[i].LeftBorder + this.Step * (intermediate1) / ((intermediate1) + (intermediate2));
        }

        /// <summary>
        /// центральный момент
        /// </summary>
        /// <param name="pow"></param>
        /// <returns></returns>
        public double FindCentralMoment(int pow)
        {
            double intermediate1 = 0;
            double intermediate2 = 0;
            for (int i = 0; i < Intervals.Count(); i++)
            {
                intermediate1 += Math.Pow(Intervals[i].MiddleOfInterval - this.Middle, pow) * Intervals[i].Frequency;
                intermediate2 += Intervals[i].Frequency;
            }
            return intermediate1 / intermediate2;
        }

        /// <summary>
        /// среднеквадратичное отклонение
        /// </summary>
        /// <returns></returns>
        public double FindMeanSquareDeviation()
        {
            return Math.Sqrt(this.FindCentralMoment(2));
        }

        /// <summary>
        /// коэффициент ассиметрии пирсона
        /// </summary>
        /// <returns></returns>
        public double FindPearsonAsymmetryCoefficient()
        {
            return (this.Middle - this.FindModa()) / this.FindMeanSquareDeviation();
        }

        /// <summary>
        /// кодффициент ассиметрии
        /// </summary>
        /// <returns></returns>
        public double FindAsymmetryCoefficient()
        {
            return this.FindCentralMoment(3) / Math.Pow(this.FindMeanSquareDeviation(), 3);
        }

        /// <summary>
        /// эксцесс
        /// </summary>
        /// <returns></returns>
        public double FindExcess()
        {
            return this.FindCentralMoment(4) / Math.Pow(this.FindMeanSquareDeviation(), 4);
        }

        /// <summary>
        /// статистика Харке-Бера
        /// </summary>
        /// <returns></returns>
        public double FindHarkeberStatistics()
        {
            double intermediate1 = Math.Pow(this.FindAsymmetryCoefficient(), 2) + Math.Pow(this.FindExcess(), 2) / 4;
            return NumExperiments / 6 * intermediate1;
        }

        /// <summary>
        /// функция гауса
        /// </summary>
        /// <param name="z"></param>
        /// <returns></returns>
        public double FindGaussianFunction(double z)
        {
            return (1 / (Math.Sqrt(2 * Math.PI))) * Math.Pow(Math.E, (z * z) / (-2));
        }

        /// <summary>
        /// согласие по критерию Пирсона
        /// </summary>
        /// <returns></returns>
        public double FindPearsonCriterionConsent()
        {
            double sampleAverage = 0;
            double samplVariance = 0;
            foreach (var el in this.Intervals)
            {
                sampleAverage += el.MiddleOfInterval * el.Frequency;
                samplVariance += Math.Pow(el.MiddleOfInterval, 2) * el.Frequency;
            }
            sampleAverage /= this.Intervals.Count();
            samplVariance /= this.Intervals.Count();
            double sampleStandardDeviation = Math.Sqrt(samplVariance);
            double intermediate1 = this.Step * this.Intervals.Count() / sampleStandardDeviation;

            double pCrit = 0;
            foreach (var el in this.Intervals)
            {
                double z = (el.MiddleOfInterval - sampleAverage) / sampleStandardDeviation;
                double fz = this.FindGaussianFunction(z);
                double ni = intermediate1 * fz;
                pCrit += Math.Pow(el.Frequency - ni, 2) / ni;
            }
            return pCrit;
        }

        /// <summary>
        /// Коэффициент корреляции Пирсона
        /// </summary>
        /// <param name="coef1"> массив коеф 1</param>
        /// <param name="coef2"> массив коеф 2</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static double FindPearsonCorrelationCoefficient(IEnumerable<double> coef1, IEnumerable<double> coef2)
        {
            if(coef1.Count() != coef2.Count()) { throw new Exception("Несоответствие массивов факторов"); }
            double x = 0;
            double y = 0;
            double xy = 0;
            double x2 = 0;
            double y2 = 0;
            int num = coef1.Count();
            for (int i = 0; i < num; i++)
            {
                x += coef1.ToArray()[i];
                y += coef2.ToArray()[i];
                xy += coef1.ToArray()[i] * coef2.ToArray()[i];
                x2 += coef1.ToArray()[i] * coef1.ToArray()[i];
                y2 += coef2.ToArray()[i] * coef2.ToArray()[i];
            }
            x /= num;
            y /= num;
            xy /= num;
            x2 /= num;
            y2 /= num;
            return (xy - x * y) / (Math.Sqrt(x2 - (x * x)) * Math.Sqrt(y2 - (y * y)));
        }
    }
}
