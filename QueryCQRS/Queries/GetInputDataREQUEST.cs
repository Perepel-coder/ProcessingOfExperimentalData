using MediatR;
using System.IO;

namespace QueryCQRS.Queries
{
    public class GetInputDataREQUEST: IRequest<GetInputDataRESPONSE>
    {
        public Stream fileStream { get; private set; }
        public GetInputDataREQUEST(Stream fileStream) { this.fileStream = fileStream; }
    }
}
