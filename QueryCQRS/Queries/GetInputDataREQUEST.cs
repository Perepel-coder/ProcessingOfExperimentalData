using MediatR;
using System.IO;

namespace QueryCQRS.Queries
{
    public class GetInputDataREQUEST: IRequest<GetInputDataRESPONSE>
    {
        public Stream FileStream { get; private set; }
        public GetInputDataREQUEST(Stream fileStream) { this.FileStream = fileStream; }
    }
}
