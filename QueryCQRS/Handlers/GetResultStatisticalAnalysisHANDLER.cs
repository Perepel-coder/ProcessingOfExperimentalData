using MediatR;
using QueryCQRS.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace QueryCQRS.Handlers
{
    public class GetResultStatisticalAnalysisHANDLER :IRequestHandler<GetResultStatisticalAnalysisREQUEST, GetResultStatisticalAnalysisRESPONSE>
    {
        public async Task<GetResultStatisticalAnalysisRESPONSE> Handle(GetResultStatisticalAnalysisREQUEST request, CancellationToken cancellationToken)
        {
            if (request.mode) { return await Task.Run(() => new GetResultStatisticalAnalysisRESPONSE(request.analysis, request.samples, request.degreesOfFreedom)); }
            return await Task.Run(() => new GetResultStatisticalAnalysisRESPONSE(request.analysis, request.firstFactor, request.secondFactor));
        }
    }
}
