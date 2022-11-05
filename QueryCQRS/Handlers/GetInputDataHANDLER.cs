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
        public bool Mode { get; set; }
        public GetInputDataHANDLER(bool getDataFromFile, IInputData inputData) 
        { 
            this.Mode = getDataFromFile;
            this.inputData = inputData;
        }
        public async Task<GetInputDataRESPONSE> Handle(GetInputDataREQUEST request, CancellationToken cancellationToken)
        {
            if (Mode == true) 
            {
                var result = await Task.Run(() => inputData.GetInputDataFromFile(request.FileStream));
                return new GetInputDataRESPONSE(result);
            }
            else { return null; }
        }
    }
}
