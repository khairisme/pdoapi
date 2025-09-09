using System.Collections.Generic;
using System.Threading.Tasks;
namespace HR.PDO.Shared.Interfaces
{
    public interface IObjectMapper
    {
        TTarget Map<TTarget>(object source) where TTarget : new();
    }
}
