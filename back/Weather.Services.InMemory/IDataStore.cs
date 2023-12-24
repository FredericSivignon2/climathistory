using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Services.InMemory
{
    public interface IDataStore
    {
        Dictionary<string, Dictionary<string, VisualCrossingData>> Data { get; }
    }
}
