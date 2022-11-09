using Autofac;
using MediatR;
using Services.Interfaces;
using System.Collections.Generic;

namespace QueryCQRS.Queries
{
    public class GetResultStatisticalAnalysisREQUEST : IRequest<GetResultStatisticalAnalysisRESPONSE>
    {
        public bool mode { get; private set; }
        public int degreesOfFreedom { get; private set; }
        public IStatisticalAnalysis analysis { get; private set; }
        public IEnumerable<double> samples { get; private set; }
        public IEnumerable<double> firstFactor { get; private set; }
        public IEnumerable<double> secondFactor { get; private set; }
        public GetResultStatisticalAnalysisREQUEST(IContainer container, IEnumerable<double> samples, int degreesOfFreedom)
        {
            this.analysis = container.Resolve<IStatisticalAnalysis>();
            this.samples = samples;
            this.degreesOfFreedom = degreesOfFreedom;
            this.mode = true;
        }
        public GetResultStatisticalAnalysisREQUEST(IContainer container, IEnumerable<double> firstFactor, IEnumerable<double> secondFactor)
        {
            this.analysis = container.Resolve<IStatisticalAnalysis>();
            this.firstFactor = firstFactor;
            this.secondFactor = secondFactor;
            this.mode = false;
        }
    }
}
