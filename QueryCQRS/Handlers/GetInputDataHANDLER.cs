using MediatR;
using QueryCQRS.Queries;
using Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace QueryCQRS.Handlers
{
    public class GetInputDataHANDLER : IRequestHandler<GetInputDataREQUEST, GetInputDataRESPONSE>
    {
        private readonly IInputData inputData;
        public bool mode { get; set; }
        public GetInputDataHANDLER(bool getDataFromFile, IInputData inputData) 
        { 
            this.mode = getDataFromFile;
            this.inputData = inputData;
        }
        public async Task<GetInputDataRESPONSE> Handle(GetInputDataREQUEST request, CancellationToken cancellationToken)
        {
            if (mode == true) 
            {
                var result = await Task.Run(() => inputData.GetInputDataFromFile(request.fileStream));
                return new GetInputDataRESPONSE(result);
            }
            else { return null; }
        }
    }
}
