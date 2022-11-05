using System.Data;
using System.IO;

namespace Services.Interfaces
{
    public interface IInputData
    {
        DataTable GetInputDataFromFile(Stream fileStream);
    }
}
