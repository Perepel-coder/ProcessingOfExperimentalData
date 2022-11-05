using MediatR;
using QueryCQRS.Queries;
using Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace QueryCQRS.Handlers
{
    public class GetStatisticalAnalysisHENDLER : IRequestHandler<GetStatisticalAnalysisREQUEST, GetStatisticalAnalysisRESPONSE>
    {
        public async Task<GetStatisticalAnalysisRESPONSE> Handle(GetStatisticalAnalysisREQUEST request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
