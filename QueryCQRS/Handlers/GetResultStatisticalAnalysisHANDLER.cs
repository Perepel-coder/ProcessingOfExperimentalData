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
            return await Task.Run(()=> request.GenerateReport());
        }
    }
}
