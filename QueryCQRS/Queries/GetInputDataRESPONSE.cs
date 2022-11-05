using System.Data;

namespace QueryCQRS.Queries
{
    public class GetInputDataRESPONSE
    {
        public DataTable InputData { get; private set; }
        public GetInputDataRESPONSE(DataTable array)
        {
            this.InputData = array;
        }
    }
}
