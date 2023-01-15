using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rainbowhut1._0.Persistances.Repositories.Common
{
    public interface ICorrelatedBy<TIdentifier>
    {
        TIdentifier Id { get; set; }
    }
}
